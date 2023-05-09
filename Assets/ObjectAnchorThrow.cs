using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ObjectAnchorThrow : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioSource Doorgrinch; // make noise if the door open
    public AudioSource DoorClick; // make noise if opemn door

    //Variable for the throwing
    //The parameter of the door
    public GameObject door; // the door to open
    public Renderer lengthdoor;
    public float length;

    //The position of the door when it is open
    public Vector3 DoorOpen;
    public Vector3 DoorOpen_final;
    public Vector3 AngleDoorOpen;

    //parameter of the speed and angle of the door
    public float doorOpenAngle = 90f; // the angle to open the door
    public float doorAngleStep = 0f;
    public float rotationSpeed = 120;
    public float moveSpeed = 90;

    public BoxCollider box;
    public bool iscollision = false;
    public bool not_position_final = false;
    public int counter = 1000;
    public int n_step = 0;
    public int n_step_close = 0;

    public bool play_door = true; //Tell if we must play the music of the door or not
    public bool door_not_open = true;

    public float certainRadius = 3;
    public float in_targ = 3 / 10;
    public target_script it_enter;

    Quaternion targetRotation_step = Quaternion.Euler(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        Vector3 size = lengthdoor.GetComponent<Renderer>().bounds.size;
        length = size.x / 2;

        // load audio
        audioSources = door.GetComponents<AudioSource>();
        Doorgrinch = audioSources[0];
        DoorClick = audioSources[1];

        //The path of the door to open it
        DoorOpen_final = new Vector3(length, 0, -length);
        //The position of the door at the beginning
        DoorOpen = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
    }

    protected void coll()
    {
        if ((it_enter.iscollision) && (door_not_open))
        {
            Quaternion targetRotation = Quaternion.Euler(0f, -doorOpenAngle, 0f);
            if (n_step < counter)
            {
                doorAngleStep = doorAngleStep + (doorOpenAngle / counter);
                targetRotation_step = Quaternion.Euler(0f, -doorAngleStep, 0f);
                DoorOpen = DoorOpen + DoorOpen_final / counter;
                door.transform.rotation = Quaternion.RotateTowards(door.transform.rotation, targetRotation_step, rotationSpeed * Time.deltaTime);
                door.transform.position = Vector3.MoveTowards(door.transform.position, DoorOpen, moveSpeed * Time.deltaTime);
                n_step++;
                if (play_door)
                {
                    DoorClick.Play();
                    Doorgrinch.Play();
                    play_door = false;
                }
            }
            if (n_step == counter)
            {
                n_step = 0;
                it_enter.reset_collision;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.LogWarningFormat("{0} Door ObjectThrow: ", DoorOpen);
        coll();
    }

    //Detect when the object collides with the target and open the door if it is the case
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Vector3 pos_rb = GetComponent<Rigidbody>().transform.position;
    //    Vector3 pos_target = collision.gameObject.transform.position;
    //    if (collision.gameObject.layer == LayerMask.NameToLayer("TargetObject"))
    //    {
    //        iscollision = true;
    //        Vector3 new_pos = new(GetComponent<Rigidbody>().transform.position.x, GetComponent<Rigidbody>().transform.position.y, GetComponent<Rigidbody>().transform.position.z + in_targ);
    //        GetComponent<Rigidbody>().transform.position = new_pos;
    //        GetComponent<Rigidbody>().isKinematic = true;
    //    }
    //}
}

