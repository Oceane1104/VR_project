using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_option : MonoBehaviour
{
    public Vector3 basic_position;
    public Quaternion basic_rotation;
    // Start is called before the first frame update
    void Start()
    {
        basic_position = this.transform.position;
        basic_rotation = this.transform.rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain")) 
        {
            this.transform.position = basic_position;
            this.transform.rotation = basic_rotation;
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
