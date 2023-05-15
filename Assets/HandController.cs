using System.Collections.Generic;
using System.Collections.Specialized;
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

	//Door script
	public Door_script door_tuto1;
    public Door_script door_tuto2;
	public bool open_now = false;
	public Timer timer;

	public Vector3[] velocitySamples = new Vector3[5];
	//private List<int> myElements;
	public int threshold = 5;
	public int count = 0;
	public bool not_first_vel = false;
	public Vector3 m_vel;
	public Vector3 addition;

    // Store all gameobjects containing an Anchor
    // N.B. This list is static as it is the same list for all hands controller
    // thus there is no need to duplicate it for each instance
    static protected ObjectAnchor[] anchors_in_the_scene;
	void Start()
	{
		// Prevent multiple fetch
		if (anchors_in_the_scene == null) anchors_in_the_scene = GameObject.FindObjectsOfType<ObjectAnchor>();
    }


	// This method checks that the hand is closed depending on the hand side
	protected bool is_hand_closed()
	{
		// Case of a left hand
		if (handType == HandType.LeftHand) return
          //OVRInput.Get(OVRInput.Button.Three)                           // Check that the A button is pressed
          //&& OVRInput.Get(OVRInput.Button.Four)  &&                        // Check that the B button is pressed
          OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5     // Check that the middle finger is pressing
		  && OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5;   // Check that the index finger is pressing


		// Case of a right hand
		else return
            //OVRInput.Get(OVRInput.Button.One)                             // Check that the A button is pressed
            //&& OVRInput.Get(OVRInput.Button.Two)   &&                        // Check that the B button is pressed
            OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) > 0.5   // Check that the middle finger is pressing
			&& OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0.5; // Check that the index finger is pressing
	}

	protected bool is_tutorial_finish()
	{
		if (handType == HandType.RightHand) return OVRInput.Get(OVRInput.Button.Four);
		else return false;
	}

    protected bool restart_game()
    {
        if (handType == HandType.LeftHand) return OVRInput.Get(OVRInput.Button.One);
        else return false;
    }

    protected Vector3 calculatorVelocity()
	{
		// get the current position of the controller
		currentPosition = this.transform.position;
		// calculate the throw direction and force based on the controller movement
		throwVelocity = (currentPosition - lastPosition)/Time.deltaTime;
		// set the last position to the current position of the controller
		lastPosition = this.transform.position;

        return throwVelocity;
	}

	protected Vector3 meanVelocity(Vector3[] velocitySamples)
	{
		addition = new Vector3 (0, 0, 0);
		for (int i = 0; i < velocitySamples.Length; i++)
		{
			addition += velocitySamples[i];
		}
		Vector3 meanVelocity = addition / velocitySamples.Length;
        Debug.LogWarningFormat("{0} the mean: ", meanVelocity);

        return meanVelocity;
	}

	void FixedUpdate()
	{
		//Before update, calculate each time the velocity of the controller
        velocity = calculatorVelocity();

		if (!(not_first_vel))
		{
			velocitySamples[count] = velocity;
			count += 1;
			if (count == velocitySamples.Length - 1)
			{
				not_first_vel = true;
				m_vel = meanVelocity(velocitySamples);
				count = 0;
			}
		}
		else
		{
			velocitySamples[count] = velocity;
			m_vel = meanVelocity(velocitySamples);
            if (count == velocitySamples.Length - 1)
			{
				count = 0;
			} else
			{
                count += 1;
            }
		}
	}

	// Automatically called at each frame
	void Update() 
	{
		//if(timer.gameFinished)
		//{
		//	if (restart_game())
		//	{
		//              timer.RestartGame();
		//          }
		//      } else
		//{
		//if (restart_game())
		//{
		//	timer.testCanva();
		//}
		//Check if we grab something
		handle_controller_behavior();
            if (is_tutorial_finish())
            {
                open_now = true;
            }
            if (open_now)
            {
                Debug.LogWarningFormat("Je suis la");
                door_tuto1.open_the_door();
                door_tuto2.open_the_door();
                timer.StartTimer();
            }

			if ((this.transform.position.z > 85) && (this.transform.position.x < -10) && (this.transform.position.x > -20))
			{
				timer.WinGame();
            }
        //}   
	}


	// Store the previous state of triggers to detect edges
	protected bool is_hand_closed_previous_frame = false;

	// Store the object atached to this hand
	// N.B. This can be extended by using a list to attach several objects at the same time
	protected ObjectAnchor object_grasped = null;

	/// <summary>
	/// This method handles the linking of object anchors to this hand controller
	/// </summary>
	protected void handle_controller_behavior()
	{

		// Check if there is a change in the grasping state (i.e. an edge) otherwise do nothing
		bool hand_closed = is_hand_closed();
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
            object_grasped.detach_from(this, m_vel);
		}
	}
}
