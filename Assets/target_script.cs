using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class target_script : MonoBehaviour
{
    //The script that will open the door
    public Door_script Thedoor;

    public float in_targ = 1;

    //Set if there is a collision and if this is the first time
    public bool first_collision = true;
    public bool its_happen = false;
    public bool good_target = false;

    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "Target_Wolf") good_target = true;
    }

    // Update is called once per frame
    void Update()
    {
        //If this is the first collision, you must open the door
        if (first_collision && its_happen && good_target)
        {
            first_collision = Thedoor.open_the_door();
        }
    }

    //Detect when the object collides with the target and open the door if it is the case
    private void OnCollisionEnter(Collision collision)
    {
        //If the collider name is TargetObject, it must stay on the target and open the door
        if ((collision.gameObject.layer == LayerMask.NameToLayer("TargetObject")))
        {
            Collider targetCollider = GetComponent<Collider>();
            if (targetCollider.bounds.Contains(collision.contacts[0].point))
            {
                // Make the rb stay on the target
                Rigidbody rb = collision.collider.attachedRigidbody;
                Vector3 new_pos = new Vector3(rb.transform.position.x + in_targ, rb.transform.position.y, rb.transform.position.z);
                rb.transform.position = new_pos;
                rb.isKinematic = true;
                its_happen = true;
            }
        }
    }
}
