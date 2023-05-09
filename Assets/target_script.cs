using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class target_script : MonoBehaviour
{
    //The script that will open the door
    public Door_script Thedoor;

    public float in_targ = 3 / 10;

    //Set if there is a collision and if this is the first time
    public bool first_collision = true;
    public bool its_happen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If this is the first collision, you must open the door
        if (first_collision && its_happen)
        {
            first_collision = Thedoor.open_the_door();
        }
    }

    //Detect when the object collides with the target and open the door if it is the case
    private void OnCollisionEnter(Collision collision)
    {
        //If the collider name is TargetObject, it must stay on the target and open the door
        if (collision.gameObject.layer == LayerMask.NameToLayer("TargetObject"))
        {
            //make the rb stay on the target
            Rigidbody rb = collision.collider.attachedRigidbody;
            Vector3 new_pos = new(rb.transform.position.x, rb.transform.position.y, rb.transform.position.z + in_targ);
            rb.transform.position = new_pos;
            rb.isKinematic = true;
            its_happen = true;
        }
    }
}
