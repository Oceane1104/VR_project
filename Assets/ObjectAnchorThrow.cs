//using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Security.Cryptography;
//using UnityEngine;
//using Debug = UnityEngine.Debug;

//public class ObjectAnchorThrow : MonoBehaviour
//{
//    public AudioSource[] audioSources;
//    public AudioSource Doorgrinch; // make noise if the door open
//    public AudioSource DoorClick; // make noise if opemn door

//    //Variable for the throwing
//    //The parameter of the door
//    public GameObject door; // the door to open
//    public Renderer lengthdoor;
//    public float length;

//    public bool iscollision = false;
//    public bool not_position_final = false;
//    public int counter = 1000;
//    public int n_step = 0;
//    public int n_step_close = 0;

//    public float certainRadius = 3;
//    public float in_targ = 3 / 10;
//    public target_script it_enter;
//    public Door_script Thedoor;

//    Quaternion targetRotation_step = Quaternion.Euler(0f, 0f, 0f);

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    protected void coll()
//    {
//        if ((it_enter.iscollision) && (door_not_open))
//        {
//            door_not_open = Thedoor.open_the_door(door, lengthdoor)
//        }
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        //Debug.LogWarningFormat("{0} Door ObjectThrow: ", DoorOpen);
//        coll();
//    }

//    //Detect when the object collides with the target and open the door if it is the case
//    //private void OnCollisionEnter(Collision collision)
//    //{
//    //    Vector3 pos_rb = GetComponent<Rigidbody>().transform.position;
//    //    Vector3 pos_target = collision.gameObject.transform.position;
//    //    if (collision.gameObject.layer == LayerMask.NameToLayer("TargetObject"))
//    //    {
//    //        iscollision = true;
//    //        Vector3 new_pos = new(GetComponent<Rigidbody>().transform.position.x, GetComponent<Rigidbody>().transform.position.y, GetComponent<Rigidbody>().transform.position.z + in_targ);
//    //        GetComponent<Rigidbody>().transform.position = new_pos;
//    //        GetComponent<Rigidbody>().isKinematic = true;
//    //    }
//    //}
//}

