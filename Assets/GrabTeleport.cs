using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Goal: grab object similar to teleportation method
// Mechanism: Press "A" (or X) to begin ray pointer
//            Press indextrigger (on ssame hand) to grab
//            If ray points to object that player can grab, object teleports into hand
//			  Release by releasing both buttons

// Overlap with handcontroller => inherit from it

public class GrabTeleport : HandController
{
	/*[Header("Maximum Distance")]
	[Range(2f, 30f)]
	// Max distance of object from player
	public float maxObjectDistance = 5f;

	[Header("Marker")]
	// Store the refence to the marker prefab used to highlight the targeted point
	public GameObject markerPrefab;
	protected GameObject marker_prefab_instanciated;

	// If already have object in hand can't grab
	protected bool teleport_grab = false;
	// check if already pointing ray
	protected bool hand_pointing = false;

	protected ObjectAnchorSpecial special_object_grasped = null;

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
	static protected ObjectAnchorSpecial[] special_anchors_in_scene;
	void Start()
	{
		// Prevent multiple fetch
		if (special_anchors_in_scene == null) special_anchors_in_scene = GameObject.FindObjectsOfType<ObjectAnchorSpecial>();
	}

	// which object to grab?
	// calculate distance
	protected int which_object(Vector3 posn)
	{
		int best_object_id = -1;
		float best_object_distance = float.MaxValue;
		float oject_distance;

		// Iterate over objects to determine if we can interact with it
		for (int i = 0; i < special_anchors_in_scene.Length; i++)
		{

			// Skip object not available
			if (!special_anchors_in_scene[i].is_available()) continue;

			// Compute the distance to the object
			oject_distance = Vector3.Distance(this.transform.position, special_anchors_in_scene[i].transform.position);

			// Keep in memory the closest object
			// N.B. We can extend this selection using priorities
			if (oject_distance < best_object_distance && oject_distance <= special_anchors_in_scene[i].get_grasping_radius())
			{
				best_object_id = i;
				best_object_distance = oject_distance;
			}
		}

		return best_object_id;
	}

	void Update()
	{
		// check if pointing
		bool pointing = is_pointing();
		bool grabbing = is_grabbing();
		if (pointing == hand_pointing && grabbing == teleport_grab) return;
		if (object_grasped || is_hand_closed()) return;
		hand_pointing = pointing; // update state
		teleport_grab = grabbing;

		// target
		Vector3 target_point;
		Vector3 forward = handController.rotation * Vector3.forward; // hand controller fwd in worldspace
		bool aim = aim_with(forward, out target_point);
		// draw ray! but only if valid point
		if (pointing && aim)
		{
			// Instantiate the marker prefab if it doesn't already exists and place it to the targeted position
			if (marker_prefab_instanciated == null) marker_prefab_instanciated = GameObject.Instantiate(markerPrefab, this.transform);
			//marker_prefab_instanciated.transform.position = target_point;

			// check if can grab object & if so pick it up
			if (grabbing)
			{
				int best_object_id = which_object(target_point);
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
	}*/
}