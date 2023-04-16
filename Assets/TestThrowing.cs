using UnityEngine;

public class TestThrowing : MonoBehaviour
{
    // Store the hand type to know which button should be pressed
    public enum HandType : int { LeftHand, RightHand };
    [Header("Hand Properties")]
    public HandType handType;

    // Store the player controller to forward it to the object
    [Header("Player Controller")]
    public MainPlayerController playerController;

    private Rigidbody grabbedObject;

    static protected ObjectAnchor[] anchors_in_the_scene;
    void Start()
    {
        // Prevent multiple fetch
        if (anchors_in_the_scene == null) anchors_in_the_scene = GameObject.FindObjectsOfType<ObjectAnchor>();
    }

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

    protected bool is_hand_open()
    {
        if (handType == HandType.LeftHand) return
                !OVRInput.Get(OVRInput.Button.Three)
                | !OVRInput.Get(OVRInput.Button.Four)
                | !(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5)
                | !(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5);
        else return
                !OVRInput.Get(OVRInput.Button.Three)
                | !OVRInput.Get(OVRInput.Button.Four)
                | !(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5)
                | !(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5);
    }

    private void Update()
    {
        if (is_hand_closed())
        {
            GrabObject();
        }

        if (is_hand_open())
        {
            ReleaseObject();
        }
    }

    private void GrabObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.collider.GetComponent<Rigidbody>() != null)
            {
                grabbedObject = hit.collider.GetComponent<Rigidbody>();
                grabbedObject.isKinematic = true;
            }
        }
    }

    private void ReleaseObject()
    {
        if (grabbedObject != null)
        {
            grabbedObject.isKinematic = false;
            grabbedObject = null;
        }
    }
}