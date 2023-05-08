using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePosn : MonoBehaviour
{
    Vector3 startPosn;
    Vector3 posn;
    Transform glued = null; // is it stuck to door?
    Vector3 doorPosn = Vector3.zero;
    public Transform target;
    bool no_move = true; // false if door is gone so can take axe w you
    bool prev_orient = false; // true if axe oriented in previous frame

    // Start is called before the first frame update
    void Start()
    {
        posn = this.transform.position;
        startPosn = posn;
        if (target)
        {
            doorPosn = target.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        posn = transform.position;
        float ogDist = (doorPosn - startPosn).magnitude;
        float dist = (posn - startPosn).magnitude;
        ObjectAnchor anchor = this.transform.GetChild(0).GetComponent<ObjectAnchor>();
        if (dist > ogDist && no_move)
        {
            Debug.Log("Begin detatch...");
            anchor.detach_from(anchor.hand_controller, Vector3.zero);
            this.transform.position = startPosn;
            Debug.LogWarningFormat("Moving axe to {0}", startPosn);
        }

        // check if this/child is held & if so face forward
        // only if haven't oriented yet
        /*else if (!anchor.is_available() && !prev_orient)
        {
            Debug.Log("Transform obj to face door");
            transform.LookAt(target);
            transform.Rotate(0, 0, -90);
            prev_orient = true;
        }else if (anchor.is_available())
        {
            prev_orient = false;
        }*/
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Debug.LogWarningFormat("Axe collided w {0}", obj.name);
        if (obj.name == "Door")
        {
            GameObject child = this.transform.GetChild(1).gameObject; // get which part of object collided
            //GameObject child = obj;
            if (child == null) Debug.Log("Null :(");
            Debug.LogWarningFormat("Collider (child) name: {0}", child.name);
            float vel = child.GetComponent<AxeCollider>().vel.magnitude;
            Debug.LogWarningFormat("Velocity: {0}", vel);

            // detatch for hand & reatatch to door
            ObjectAnchor anchor = transform.GetChild(0).gameObject.GetComponent<ObjectAnchor>(); // anchor belongs to handle
            if (anchor == null) { Debug.Log("No anchor??"); return; }// no anchor?
            anchor.detach_from(anchor.hand_controller, Vector3.zero);

            // glue to door
            GetComponent<Rigidbody>().isKinematic = true;
            if (glued == null || glued != collision.gameObject.transform)
            {
                posn = collision.gameObject.transform.position - transform.position;
                glued = collision.gameObject.transform;
                Debug.LogWarningFormat("Glued to posn {0}", posn);
            }

            int ret = obj.GetComponent<BreakDoor>().onBladeHit(child);

            if (ret == 1)
            {
                Debug.Log("Destroyed door, do it again?");
                if (obj)
                {
                    Destroy(obj);
                    Debug.Log("Done it again!!");
                }
                no_move = false;
            }
        }
    }
}
