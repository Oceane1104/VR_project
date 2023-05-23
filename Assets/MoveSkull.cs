using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSkull : MonoBehaviour
{
    Vector3 posn;


    void Start()
    {
        posn = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.parent && transform.parent.name.Contains("ControllerAnchor")){
            Debug.Log("Is attached");
            if (transform.childCount > 0)
            {
                transform.GetChild(0).position = transform.parent.position;
            }
            else
            {
                transform.position = transform.parent.position;
            }
            
        }   
    }
}
