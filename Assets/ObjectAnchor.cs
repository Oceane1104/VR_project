using System.Collections;
using System.Collections.Specialized;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ObjectAnchor : MonoBehaviour
{
	[Header("Grasping Properties")]
	public float graspingRadius = 0.1f;
    public float throwForce = 1f; //Force apply to the object when throwing
    public float floorLimit = 0.0f; // set this to the y-position of your floor
    public OVRInput.Controller controller;

    public float throwForceMultiplier = 1f; // the multiplier for the throw force

    private Vector3 lastPosition;// the last position of the controller

    Vector3 previous;
    Vector3 velocity;

    // Store initial transform parent
    protected Transform initial_transform_parent;
	void Start()
	{
        initial_transform_parent = transform.parent;
    }


	// Store the hand controller this object will be attached to
	protected HandController hand_controller = null;

	public void attach_to(HandController hand_controller)
	{
		// Store the hand controller in memory
		this.hand_controller = hand_controller;

        //Disable the Rigidbody component of the object
        GetComponent<Rigidbody>().isKinematic = true;

        // Set the object to be placed in the hand controller referential
        transform.SetParent(hand_controller.transform);    
    }

	public void detach_from(HandController hand_controller)
	{
		// Make sure that the right hand controller ask for the release
		if (this.hand_controller != hand_controller) return;

		// Detach the hand controller
		this.hand_controller = null;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;

        // get the current position of the controller
        Vector3 currentPosition = hand_controller.transform.forward;

        // calculate the throw direction and force based on the controller movement
        Vector3 throwDirection = currentPosition - lastPosition;

        OVRInput.Controller ctrl = hand_controller.handType == HandController.HandType.LeftHand ? OVRInput.Controller.LHand : OVRInput.Controller.RHand; // Si main droite ou main gauche
        Vector3 acceleration = OVRInput.GetLocalControllerAcceleration(ctrl);


		// normalize the throw direction and apply the throw force to the object
		//throwDirection.Normalize();
		GetComponent<Rigidbody>().AddForce(acceleration * throwForce, ForceMode.Impulse);

        // set the last position to the current position of the controller
        lastPosition = hand_controller.transform.forward;

        // clamp the y-position of the Rigidbody to the floor limit
        Vector3 clampedPosition = GetComponent<Rigidbody>().position;
        clampedPosition.y = Mathf.Max(clampedPosition.y, floorLimit);
        GetComponent<Rigidbody>().position = clampedPosition;

        // stop the Rigidbody's movement when the trigger is released
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

        //OnCollisionEnter(GetComponent<Rigidbody>().OnCollisionEnter());
    }

    //Detect when the object collides with the target
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            //isStuck = true; //Tell us if the object is stuck on the target
            GetComponent<Rigidbody>().isKinematic = true; //Disable the Rigidbody component of the object
            //transform.position = target.position; //Set the object on the position of the target
            //transform.rotation = target.rotation; //Set the object on the same rotation of the targer
        }
    }

    public bool is_available() { return hand_controller == null; }

	public float get_grasping_radius() { return graspingRadius; }
    //public float get_throwForce() { return throwForce; }
}
