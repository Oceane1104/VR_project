using UnityEngine;

public class checkgravity : MonoBehaviour
{
    private void Start()
    {
        // Check if a Rigidbody component is attached to this GameObject
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody != null)
        {
            // Print the enabled state of the Rigidbody
            Debug.Log("Rigidbody Enabled: yes" );
        }
        else
        {
            Debug.Log("No Rigidbody found on this GameObject.");
        }
        Debug.LogWarning("child number: " + transform.childCount);
    }
}