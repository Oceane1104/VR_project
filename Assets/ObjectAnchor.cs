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
    public AudioSource audioSources_throw;

    // Store initial transform parent
    protected Transform initial_transform_parent;
	void Start()
	{
        AudioSource[] audioSources_rb = this.GetComponents<AudioSource>();
        audioSources_throw = audioSources_rb[0];
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
        this.transform.SetParent(hand_controller.transform);    
    }

	public void detach_from(HandController hand_controller, Vector3 velocity)
	{
        vel = velocity.magnitude;
        throwDirection = transform.forward;

        if (vel > 2)
        {
            audioSources_throw.Play();
        }
        // Make sure that the right hand controller ask for the release
        if (this.hand_controller != hand_controller) return;

		// Detach the hand controller
		this.hand_controller = null;
        transform.SetParent(null);
        GetComponent<Rigidbody>().isKinematic = false;

        Debug.LogWarningFormat("{0} velocity: ", velocity);
        Debug.LogWarningFormat("{0} force: ", velocity*throwForce);

        //Through the object with the velocity of the controller
        GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);

        // clamp the y-position of the Rigidbody to the floor limit
        Vector3 clampedPosition = GetComponent<Rigidbody>().position;
        clampedPosition.y = Mathf.Max(clampedPosition.y, floorLimit);
        GetComponent<Rigidbody>().position = clampedPosition;

        // stop the Rigidbody's movement when the trigger is released
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
    public bool is_available() { return hand_controller == null; }

	public float get_grasping_radius() { return graspingRadius; }
}
