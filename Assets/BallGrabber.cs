using UnityEngine;

public class BallGrabber : MonoBehaviour
{
    public Transform bucketTransform; // The transform of the bucket game object

    private bool canCatch = false; // Flag to determine if the cube can be caught
    private bool hasCube = false; // Flag to determine if the player is holding the cube
    private Rigidbody cubeRigidbody; // The rigidbody of the cube game object

    private void Start()
    {
        // Get the rigidbody component of the cube game object
        cubeRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // If the player collides with a cube, set the canCatch flag to true
        if (other.gameObject.CompareTag("Cube"))
        {
            canCatch = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // If the player exits the collider of the cube, set the canCatch flag to false
        if (other.gameObject.CompareTag("Cube"))
        {
            canCatch = false;
        }
    }

    private void Update()
    {
        // If the player clicks the left mouse button and can catch the cube, pick up the cube
        if (Input.GetMouseButtonDown(0) && canCatch && !hasCube)
        {
            cubeRigidbody.isKinematic = true; // Make the cube kinematic
            cubeRigidbody.transform.parent = transform; // Set the cube as a child of the player object
            hasCube = true; // Set the hasCube flag to true
        }

        // If the player clicks the left mouse button and has the cube, put the cube in the bucket
        if (Input.GetMouseButtonDown(0) && hasCube)
        {
            cubeRigidbody.transform.parent = null; // Remove the cube as a child of the player object
            cubeRigidbody.isKinematic = false; // Make the cube non-kinematic
            cubeRigidbody.AddForce(transform.forward * 500f); // Add a force to the cube to throw it into the bucket
            hasCube = false; // Set the hasCube flag to false
        }
    }
}