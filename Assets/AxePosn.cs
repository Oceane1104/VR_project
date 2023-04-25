using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePosn : MonoBehaviour
{
    Vector3 startPosn;
    Vector3 posn;
    Transform glued = null; // is it stuck to door?
    public Vector3 doorPosn = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        posn = this.transform.position;
        startPosn = posn;
    }

    // Update is called once per frame
    void Update()
    {
        posn = transform.position;
        {
            float ogDist = (doorPosn - startPosn).magnitude;
            float dist = (posn - startPosn).magnitude;
            if (dist > ogDist)
            {
                Debug.Log("Begin detatch...");
                ObjectAnchor anchor = GetComponent<ObjectAnchor>();
                anchor.detach_from(anchor.hand_controller, Vector3.zero);
                this.transform.position = startPosn;
                Debug.LogWarningFormat("Moving axe to {0}", startPosn);
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject obj = collision.gameObject;
        Debug.LogWarningFormat("Axe collided w {0}", obj.name);
        if (obj.name == "Door")
        {
            GameObject child = this.transform.GetChild(1).gameObject; // get which part of object collided
            if (child == null) Debug.Log("Null :(");
            Debug.LogWarningFormat("Collider (child) name: {0}", child.name);
            float vel = child.GetComponent<AxeCollider>().vel.magnitude;
            Debug.LogWarningFormat("Velocity: {0}", vel);

            // detatch for hand & reatatch to door
            ObjectAnchor anchor = gameObject.GetComponent<ObjectAnchor>();
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
            }
        }
    }
}
