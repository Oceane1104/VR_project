using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ObjectAnchor : MonoBehaviour
{
	[Header("Grasping Properties")]
	public float graspingRadius = 0.1f; //The radius at which you can grab the object
    public float throwForce = 1; //Force to multiply to the object when throwing (if you need to be less or more stronger)
    public float floorLimit = 0.0f; // set this to the y-position of your floor for the rigidbody don't go through the floor
    public float vel;
    public Vector3 throwDirection;
	public float teleportRadius = 1.5f;
	public bool can_teleport = true;
	public bool need_parent = false;

	// instead of having object distance, radius, etc be defined here
	// have specific object deal with wheter it is "close enough"
	// reason: allows for more complex logic (ex. if long object) without many if clauses
	// Keep in mind: should thus have the same name and input variables; can add extra but make them optional
	// Function: bool getDistance(Vector3 posn, out float dist) where posn is the hand controller position,
	//				dist is the object_distance to be modified, and the function returns
	//				a bool which is true if the object is "close enough" to be grasped
	public string objScript = "ObjectAnchor"; // default

	// Store initial transform parent
	protected Transform initial_transform_parent;
	void Start()
	{
		initial_transform_parent = transform.parent;
	}

	// Store the hand controller this object will be attached to
	public HandController hand_controller = null;

	public void attach_to(HandController hand_controller)
	{
		GameObject parent = null;
		if (transform.parent && need_parent)
		{
			parent = transform.parent.gameObject;
		}

		Debug.LogWarningFormat("Regular attaching... Object has parent: {0}", parent != null);
		// Store the hand controller in memory
		this.hand_controller = hand_controller;

		//Disable the Rigidbody component of the object
		if (parent && parent.GetComponent<Rigidbody>())
        {
			parent.GetComponent<Rigidbody>().isKinematic = true;
        }
		else if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().isKinematic = true;
		}

		// Set the object to be placed in the hand controller referential
		if (parent)
        {
			parent.transform.SetParent(hand_controller.transform);
        }
        else
        {
			transform.SetParent(hand_controller.transform);
		}
	}

	public void detach_from(HandController hand_controller, Vector3 velocity)
	{
        vel = velocity.magnitude;
        throwDirection = transform.forward;

        // Make sure that the right hand controller ask for the release
        if (this.hand_controller != hand_controller) return;

		GameObject parent = null;
		if (transform.parent && need_parent)
		{
			parent = transform.parent.gameObject;
			parent.transform.SetParent(null);
        }
        else
        {
			transform.SetParent(null);
        }

		// Detach the hand controller
		this.hand_controller = null;
		
		if (parent && parent.GetComponent<Rigidbody>())
        {
			parent.GetComponent<Rigidbody>().isKinematic = false;
		}  else if (GetComponent<Rigidbody>())
		{
			GetComponent<Rigidbody>().isKinematic = false;
			//Through the object with the velocity of the controller
			GetComponent<Rigidbody>().AddForce(velocity * throwForce, ForceMode.Impulse);

			// clamp the y-position of the Rigidbody to the floor limit
			Vector3 clampedPosition = GetComponent<Rigidbody>().position;
			clampedPosition.y = Mathf.Max(clampedPosition.y, floorLimit);
			GetComponent<Rigidbody>().position = clampedPosition;

			// stop the Rigidbody's movement when the trigger is released
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
		}
		
		Debug.Log("Detatching...");
	}

	public bool getDistance(Vector3 posn, out float object_distance, bool teleport = false)
    {
		object_distance = Vector3.Distance(posn, transform.position);
		if (teleport) return (object_distance < teleportRadius);
		return (object_distance < graspingRadius);
	}

	public bool is_available() { return hand_controller == null; }

	public float get_grasping_radius() { return graspingRadius; }

	public float get_teleport_radius() { return teleportRadius; }

	public void teleport_attach_to(HandController hand_controller)
	{
		// call regular attach function
		attach_to(hand_controller);

		// also make sure object "flies to" hand
		Debug.LogWarningFormat("Moving object {2} from {0} to {1}", transform.position, hand_controller.transform.position, transform.gameObject.name);

		//transform.position = hand_controller.transform.position;
		if (transform.parent && !gameObject.CompareTag("ColliderBucket"))
		{
			transform.parent.position = hand_controller.transform.position;
			
		}
        else
        {
			transform.position = hand_controller.transform.position;
			
		}

		Debug.LogWarningFormat("New posn: {0} vs parent {1}", transform.position, transform.parent.position);
	}

}
