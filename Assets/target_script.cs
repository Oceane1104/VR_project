using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class target_script : MonoBehaviour
{

    public AudioSource[] audioSources;
    public AudioSource Doorgrinch;
    public AudioSource DoorClick;

    //Variable for the throwing
    //The parameter of the door
    public GameObject door; // the door to open
    public Renderer lengthdoor;
    public float length;

    public Renderer lengthtarg;
    public float lengthtarget;

    //The position of the door when it is open
    public Vector3 DoorOpen;
    public Vector3 DoorOpen_final;
    public Vector3 AngleDoorOpen;

    //parameter of the speed and angle of the door
    public float doorOpenAngle = 90f; // the angle to open the door
    public float doorAngleStep = 0f;
    public float rotationSpeed = 120;
    public float moveSpeed = 90;

    public bool iscollision = false;
    public bool not_position_final = false;
    public int counter = 1000;
    public int n_step = 0;

    public bool play_door = true; //Tell if we must play the music of the door or not

    Quaternion targetRotation_step = Quaternion.Euler(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        //Comoute the length of the door
        Vector3 size = lengthdoor.GetComponent<Renderer>().bounds.size;
        length = size.x / 2;

        Vector3 sizetarg = lengthtarg.GetComponent<Renderer>().bounds.size;
        length = sizetarg.y / 2;

        // load audio
        // Get the Audio Source component on the door object
        AudioSource audioSource = door.GetComponent<AudioSource>();
        Doorgrinch = audioSources[0];
        DoorClick = audioSources[1];

        //Compute the initial position of the door and the last position that we want
        DoorOpen_final = new Vector3(length, 0, -length);
        DoorOpen = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
    }

    protected void coll()
    {
        if (iscollision)
        {
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
            ////Keep the collider on the target
            //GetComponent<Rigidbody>().isKinematic = true;
            //GetComponent<Rigidbody>().transform.position = this.transform.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.LogWarningFormat("{0} Door target: ", DoorOpen);
        coll();
    }

    //Detect when the object collides with the target and open the door if it is the case
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.layer == LayerMask.NameToLayer("ThrowObject"))
        //{
            iscollision = true;
        //Keep the collider on the target
        collision.collider.attachedRigidbody.isKinematic = true;
        if (collision.collider.attachedRigidbody.name == "TargetStyle")
        {
            Vector3 new_pos = new(this.transform.position.x, this.transform.position.y + lengthtarget, this.transform.position.z);
            collision.collider.attachedRigidbody.transform.position = new_pos;
        } else
        {
            collision.collider.attachedRigidbody.transform.position = this.transform.position;
        }

        //}
    }
}
