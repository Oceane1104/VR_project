using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hold_weapon : MonoBehaviour
{
    public Rigidbody weapon1;
    public Rigidbody weapon2;
    public Rigidbody weapon3;
    public Rigidbody weapon4;

    // Start is called before the first frame update
    void Start()
    {
        weapon1.isKinematic = true;
        weapon2.isKinematic = true;
        weapon3.isKinematic = true;
        weapon4.isKinematic = true;
    }
}
