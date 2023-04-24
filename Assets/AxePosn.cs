using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePosn : MonoBehaviour
{
    Vector3 startPosn;
    Vector3 posn;
    public GameObject doorObject; // store door here so can compare locations

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
        if (doorObject) // make sure object not destroyed/etc
        {
            Vector3 doorPosn = doorObject.transform.position;
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
}
