using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using UnityEngine;

public class Throwing : MonoBehaviour
{
    //public float throwForce = 10f; //Force apply to the object when throwing
    //private Rigidbody rb; //Reference to the Rigidbody component of the object
    ////private bool isStuck = false; //Tell us if the object is on the target
    //private Transform target; //Reference to the target object

    //public enum HandType : int { LeftHand, RightHand };
    //[Header("Hand Properties")]
    //public HandType handType;
    //public bool holdingObject = false;
    //public HandController handcontroller;

    ////Start is called before the first frame update
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody>(); //Get the reference to the Rigidbody component
    //    target = GameObject.FindGameObjectWithTag("Target").transform; //Get the reference to the target object
    //}

    //protected bool is_hand_open()
    //{
    //    if (handType == HandType.LeftHand) return
    //            !OVRInput.Get(OVRInput.Button.Three)
    //            | !OVRInput.Get(OVRInput.Button.Four)
    //            | !(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5)
    //            | !(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5);
    //    else return
    //            !OVRInput.Get(OVRInput.Button.Three)
    //            | !OVRInput.Get(OVRInput.Button.Four)
    //            | !(OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) > 0.5)
    //            | !(OVRInput.Get(OVRInput.Axis1D.PrimaryIndexTrigger) > 0.5);
    //}

    ////Update is called once per frame
    //void Update()
    //{
    //    holdingObject = handcontroller.is_grab;
    //    if (is_hand_open() && holdingObject)
    //    {
    //        //Calculate the throw direction based on the forward direction of the controller
    //        Vector3 throwDirection = transform.forward;

    //        //Apply the throw force to the object in the calculated direction
    //        rb.AddForce(throwDirection * throwForce, ForceMode.Impulse);

    //        holdingObject = false; //Tell us that the object is no more held
    //    }
    //}

    ////Detect when the object collides with the target
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Target"))
    //    {
    //        //isStuck = true; //Tell us if the object is stuck on the target
    //        rb.isKinematic = true; //Disable the Rigidbody component of the object
    //        transform.position = target.position; //Set the object on the position of the target
    //        transform.rotation = target.rotation; //Set the object on the same rotation of the targer
    //    }
    //}
}