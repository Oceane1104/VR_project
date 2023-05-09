using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class target_script : MonoBehaviour
{
    public bool iscollision = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void reset_collision()
    {
        iscollision = false;
    }

    //Detect when the object collides with the target and open the door if it is the case
    private void OnCollisionEnter(Collision collision)
    {
        Vector3 pos_rb = GetComponent<Rigidbody>().transform.position;
        Vector3 pos_target = collision.gameObject.transform.position;

        if (collision.gameObject.layer == LayerMask.NameToLayer("TargetObject"))
        {
            iscollision = true;
        }
    }
}
