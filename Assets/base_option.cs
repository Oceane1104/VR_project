using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class base_option : MonoBehaviour
{

    public Quaternion basic_rotation;
    public Vector3 basic_position;

    // Start is called before the first frame update
    void Start()
    {
        basic_position = this.transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Terrain")
        {
            this.transform.position = basic_position;
            this.transform.rotation = basic_rotation;
            this.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
