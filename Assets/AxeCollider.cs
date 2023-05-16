using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeCollider : MonoBehaviour
{
    Vector3 posn = Vector3.zero; // keep track of posn to determine velocity
    public Vector3 vel = Vector3.zero; // velocity of object

    // Start is called before the first frame update
    void Start()
    {
        posn = transform.position;
    }

    // update velocity 
    void Update()
    {
        Vector3 dist = transform.position - posn;
        posn = transform.position;

        Vector3 temp = dist / Time.deltaTime;
        if (temp.magnitude != 0) vel = temp;
    }
}
