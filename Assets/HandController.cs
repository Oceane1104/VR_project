using System.Threading;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class HandController : MonoBehaviour
{

	// Store the hand type to know which button should be pressed
	public enum HandType : int { LeftHand, RightHand };
	[Header("Hand Properties")]
	public HandType handType;

	// Store the player controller to forward it to the object
	[Header("Player Controller")]
	public MainPlayerController playerController;

	//The variable for the calculator of the velocity of the controller
	public Vector3 velocity;
	public Vector3 lastPosition;
	public Vector3 throwVelocity;
	public Vector3 currentPosition;

	[Header("Maximum Distance")]
	[Range(2f, 30f)]
	// Max distance of object from player
	public float maxObjectDistance = 15f;

	[Header("Marker")]
	// Store the refence to the marker prefab used to highlight the targeted point
	public GameObject markerPrefab;
	protected GameObject marker_prefab_instanciated;

	// If already have object in hand can't grab
	protected bool teleport_grab = false;
	// check if already pointing ray
	protected bool hand_pointing = false;

	protected ObjectAnchor special_object_grasped = null;

	// return true if either hand pointing 
	protected bool is_pointing()
	{
		if (handType == HandType.LeftHand)
		{
			return OVRInput.Get(OVRInput.Button.Three);
		}

		return OVRInput.Get(OVRInput.Button.One);
	}

	bool is_grabbing()
	{
		if (handType == HandType.LeftHand)
		{
			return OVRInput.Get(OVRInput.Button.Three) &&
				OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5;
		}

		return OVRInput.Get(OVRInput.Button.One) &&
							OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5;
	}

	// Store all gameobjects containing an Anchor
	static protected ObjectAnchor[] special_anchors_in_scene;

	// Store all gameobjects containing an Anchor
	// N.B. This list is static as it is the same list for all hands controller
	// thus there is no need to duplicate it for each instance
	static protected ObjectAnchor[] anchors_in_the_scene;
	void Start()
	{
		// Prevent multiple fetch
		if (anchors_in_the_scene == null) anchors_in_the_scene = GameObject.FindObjectsOfType<ObjectAnchor>();
		if (special_anchors_in_scene == null) {
			var temp = new System.Collections.Generic.List<ObjectAnchor>();
			foreach(ObjectAnchor obj in anchors_in_the_scene)
            {
				temp.Add(obj);
            }
			special_anchors_in_scene = temp.ToArray();
		}

	}


	// This method checks that the hand is closed depending on the hand side
	protected bool is_hand_closed()
	{
		// Case of a left hand
		if (handType == HandType.LeftHand) return
		  OVRInput.Get(OVRInput.Button.Three)                           // Check that the A button is pressed
		  && OVRInput.Get(OVRInput.Button.Four)                         // Check that the B button is pressed
		  && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5     // Check that the middle finger is pressing
		  && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5;   // Check that the index finger is pressing


		// Case of a right hand
		else return
			OVRInput.Get(OVRInput.Button.One)                             // Check that the A button is pressed
			&& OVRInput.Get(OVRInput.Button.Two)                          // Check that the B button is pressed
			&& OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5   // Check that the middle finger is pressing
			&& OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5; // Check that the index finger is pressing
	}

	protected Vector3 calculatorVelocity()
	{
		// get the current position of the controller
		currentPosition = this.transform.position;
		// calculate the throw direction and force based on the controller movement
		throwVelocity = (currentPosition - lastPosition) / Time.deltaTime;
		// set the last position to the current position of the controller
		lastPosition = this.transform.position;

		return throwVelocity;
	}

	// which object to grab?
	// calculate distance
	protected int which_object(Vector3 posn)
	{
		int best_object_id = -1;
		float best_object_distance = float.MaxValue;
		float object_distance;

		// Iterate over objects to determine if we can interact with it
		for (int i = 0; i < special_anchors_in_scene.Length; i++)
		{
			// Skip object not available
			if (!special_anchors_in_scene[i].is_available()) continue;

			// Compute the distance to the object
			object_distance = Vector3.Distance(posn, special_anchors_in_scene[i].transform.position);
			Debug.LogWarningFormat("Object {0} distance: {1}", special_anchors_in_scene[i].transform.gameObject.name, object_distance);

			// Keep in memory the closest object
			// N.B. We can extend this selection using priorities
			if (object_distance < best_object_distance && object_distance <= special_anchors_in_scene[i].get_teleport_radius())
			{
				best_object_id = i;
				best_object_distance = object_distance;
			}
		}

		return best_object_id;
	}


	void FixedUpdate()
	{
		velocity = calculatorVelocity();
	}

	// Automatically called at each frame
	void Update()
	{
		handle_controller_behavior();
		handle_teleport_behavior();
	}


	// Store the previous state of triggers to detect edges
	protected bool is_hand_closed_previous_frame = false;

	protected void handle_teleport_behavior()
    {
		if (object_grasped || is_hand_closed()) return; // make sure not regular grab

		bool pointing = is_pointing(); // pointing and/or grabbing
		bool grabbing = is_grabbing();

		if (pointing == hand_pointing && grabbing == teleport_grab) return; // no change
		hand_pointing = pointing; // update state
		teleport_grab = grabbing;

		// target
		Vector3 target_point;
		Vector3 forward = handController.rotation * Vector3.forward; // hand controller fwd in worldspace
		bool aim = aim_with(forward, out target_point);
		// draw ray! but only if valid point/hand not completely closed
		if (pointing && aim)
		{
			// Instantiate the marker prefab if it doesn't already exists and place it to the targeted position
			if (marker_prefab_instanciated == null) marker_prefab_instanciated = GameObject.Instantiate(markerPrefab, this.transform);

			//marker_prefab_instanciated.transform.position = target_point;

			// check if can grab object & if so pick it up
			if (grabbing)
			{
				int best_object_id = which_object(marker_prefab_instanciated.transform.position);//target_point);
				// If the best object is in range grab it
				if (best_object_id != -1)
				{

					// Store in memory the object grasped
					special_object_grasped = special_anchors_in_scene[best_object_id];

					// Log the grasp
					Debug.LogWarningFormat("{0} grasped {1}", this.transform.parent.name, special_object_grasped.name);

					// Grab this object
					special_object_grasped.teleport_attach_to(this);
				}
			}
		}

		// what if hand is open?
		if (special_object_grasped != null && !grabbing)
		{
			// Log the release
			Debug.LogWarningFormat("{0} released {1}", this.transform.parent.name, special_object_grasped.name);

			// Release the object
			special_object_grasped.detach_from(this, velocity);
			special_object_grasped = null;
		}

		// if not pointing, out of range, or have object in hand
		if (!pointing || !aim || special_object_grasped != null)
		{
			// Remove the cursor
			if (marker_prefab_instanciated != null) Destroy(marker_prefab_instanciated);
			marker_prefab_instanciated = null;
		}
	}

	public Transform handController; // handcontroller posn so know where you're pointing

	protected bool aim_with(Vector3 forward, out Vector3 target_point)
	{

		// Default the "output" target point to the null vector
		target_point = new Vector3();

		// Launch the ray cast and leave if it doesn't hit anything
		RaycastHit hit;
		if (!Physics.Raycast(this.transform.position, forward, out hit, Mathf.Infinity)) return false;

		// If the aimed point is out of range (i.e. the raycast distance is above the maximum distance) then prevent the grab
		if (hit.distance > maxObjectDistance) return false;

		// "Output" the target point
		target_point = hit.point;
		return true;
	}

	// Store the object atached to this hand
	// N.B. This can be extended by using a list to attach several objects at the same time
	protected ObjectAnchor object_grasped = null;

	/// <summary>
	/// This method handles the linking of object anchors to this hand controller
	/// </summary>
	protected void handle_controller_behavior()
	{
		// check if teleport-grab case instead

		bool hand_closed = is_hand_closed();

		//bool pointing = is_pointing() && !hand_closed; // make sure only pointing/grabbing but hand not closed

		//if (pointing && object_grasped == null) return; // if non-null need to drop object
		if (special_object_grasped) return;

		// Check if there is a change in the grasping state (i.e. an edge) otherwise do nothing
		if (hand_closed == is_hand_closed_previous_frame) return;
		is_hand_closed_previous_frame = hand_closed;

		//==============================================//
		// Define the behavior when the hand get closed //
		//==============================================//
		if (hand_closed)
		{

			// Log hand action detection
			Debug.LogWarningFormat("{0} get closed", this.transform.parent.name);

			// Determine which object available is the closest from the left hand
			int best_object_id = -1;
			float best_object_distance = float.MaxValue;
			float oject_distance;

			// Iterate over objects to determine if we can interact with it
			for (int i = 0; i < anchors_in_the_scene.Length; i++)
			{

				// Skip object not available
				if (!anchors_in_the_scene[i].is_available()) continue;

				// Compute the distance to the object
				oject_distance = Vector3.Distance(this.transform.position, anchors_in_the_scene[i].transform.position);

				// Keep in memory the closest object
				// N.B. We can extend this selection using priorities
				if (oject_distance < best_object_distance && oject_distance <= anchors_in_the_scene[i].get_grasping_radius())
				{
					best_object_id = i;
					best_object_distance = oject_distance;
				}
			}

			// If the best object is in range grab it
			if (best_object_id != -1)
			{

				// Store in memory the object grasped
				object_grasped = anchors_in_the_scene[best_object_id];

				// Log the grasp
				Debug.LogWarningFormat("{0} grasped {1}", this.transform.parent.name, object_grasped.name);

				// Grab this object
				object_grasped.attach_to(this);
			}



			//==============================================//
			// Define the behavior when the hand get opened //
			//==============================================//
		}
		else if (object_grasped != null)
		{
			// Log the release
			Debug.LogWarningFormat("{0} released {1}", this.transform.parent.name, object_grasped.name);

			// Release the object
			object_grasped.detach_from(this, velocity);
			object_grasped = null;
		}
	}
}