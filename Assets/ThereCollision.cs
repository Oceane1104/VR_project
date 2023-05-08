//using System.Collections;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Reflection;
//using System.Threading;
//using UnityEngine;

//public class ThereCollision : MonoBehaviour
//{
//    //    public GameObject door; // the door to open
//    //    public float doorOpenAngle = 90f; // the angle to open the door
//    //    public float rotationSpeed = 90;
//    //    public float moveSpeed = 90;

//    //    public Renderer lengthdoor;
//    //    public float length;
//    public bool is_trigger = false;
//    protected ObjectAnchorThrow test;

    //    public Vector3 DoorOpen;
    //    public Vector3 AngleDoorOpen;

    //    public bool iscollision = false;

    //    public float degrees = -90f;

    //    private float startTime;

    //    public Collision target;

    //    public BoxCollider box;

    //    protected void coll()
    //    {
    //        if (iscollision)
    //        {
    //            //float axey = door.transform.position.y;
    //            //float axez = door.transform.position.z;
    //            //Vector3 centerofrotation = new Vector3(length, axey, axez);
    //            //// apply the rotation around the pivot point
    //            //door.transform.RotateAround(centerofrotation, Vector3.up, rotationSpeed * Time.deltaTime);


    //            //door.transform.rotation = Vector3.MoveTowards(door.transform.rotation, AngleDoorOpen, rotationSpeed * Time.deltaTime);
    //            //rotate non smoothly the door
    //            door.transform.position = Vector3.MoveTowards(door.transform.position, DoorOpen, moveSpeed * Time.deltaTime);
    //            door.transform.rotation = Quaternion.RotateTowards(door.transform.rotation, Quaternion.Euler(AngleDoorOpen), rotationSpeed * Time.deltaTime);
    //            //target.attachedRigidbody.isKinematic = true;

    //            //box.attachedRigidbody.isKinematic = true;
    //            //// rotate the door to the open position
    //            //box.attachedRigidbody.transform.SetParent(box.transform);

    //            target.collider.transform.position = new Vector3(0, 0, 0);
    //            //this.transform.position = new Vector3(0, 0, 0);

    //            //target.attachedRigidbody.transform.SetParent(target.transform);
    //            //GetComponent<Rigidbody>().transform.position = target.transform.position;
    //        }

    //    }


    //    // Start is called before the first frame update
    //    void Start()
    //    {
    //        Vector3 size = lengthdoor.GetComponent<Renderer>().bounds.size;
    //        length = size.x/2;

    //        DoorOpen = new Vector3(door.transform.position.x + length, door.transform.position.y, door.transform.position.z - length);
    //        AngleDoorOpen = new Vector3(door.transform.rotation.x, door.transform.rotation.y + 90, door.transform.rotation.z);

    //        startTime = Time.time;
    //    }

    //    // Update is called once per frame
//    void Update()
//    {
//        //if (is_trigger)
//        //{
//        //    is_trigger = test.coll();
//        //}
//    }

//    //    //Detect when the object collides with the target
//    //    private void OnCollisionEnter(Collision collision)
//    //    {
//    //        iscollision = true;
//    //    }

//    public void set_is_trigger()
//    {
//        is_trigger = false;
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        is_trigger = true;
//        //Vector3 pos_rb = GetComponent<Rigidbody>().transform.position;
//        //Vector3 pos_target = other.gameObject.transform.position;
//        //if (other.gameObject.layer == LayerMask.NameToLayer("TargetObject"))
//        //{
//        //    iscollision = true;
//        //    //Keep the collider on the target
//        //    Vector3 new_pos = new(GetComponent<Rigidbody>().transform.position.x, GetComponent<Rigidbody>().transform.position.y + in_targ, GetComponent<Rigidbody>().transform.position.z);
//        //    GetComponent<Rigidbody>().transform.position = new_pos;
//        //    GetComponent<Rigidbody>().isKinematic = true;
//        //}
//    }

//}
