using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class Door_script : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioSource Doorgrinch; // make noise of grinch
    public AudioSource DoorClick; // make noise of click

    //parameter of the speed and angle of the door
    public float doorOpenAngle = 90f; // the angle to open the door
    public float doorAngleStep = 0f;
    public float rotationSpeed = 120; 
    public float moveSpeed = 90; 

    //The position of the door when it is open
    public Vector3 DoorOpen;
    public Vector3 DoorOpen_final;
    public Quaternion targetRotation_step;
    public float length;

    public bool play_door = true; //Tell if we must play the music of the door or not
    public bool door_not_open = true;
    public int n_step = 0;
    public int counter = 500;

    public float Size_X;
    public float Size_Y;
    public float Size_Z;

    public float TheRotationBase;
    private BoxCollider boxCollider;

    void Start()
    {
        TheRotationBase = this.transform.rotation.eulerAngles.y;
        Debug.LogWarningFormat("{0} the rotation: ", TheRotationBase);
        boxCollider = GetComponent<BoxCollider>();
        Size_X = boxCollider.size.x;
        Size_Y = boxCollider.size.y;
        Size_Z = boxCollider.size.z;
    }

    public bool open_the_door () 
    {
        //Initialisation
        if (n_step == 0)
        {
            //The half length of the door
            Vector3 size = this.GetComponent<Renderer>().bounds.size;


            if (TheRotationBase == 0)
            {
                length = size.x / 2;
                //The path of the door to open it
                DoorOpen_final = new Vector3(length, 0, length);
            }
            else if (TheRotationBase == 90)
            {
                length = size.z / 2;
                //The path of the door to open it
                DoorOpen_final = new Vector3(length, 0, -length);
            }
            else if (TheRotationBase == 180)
            {
                length = size.x / 2;
                //The path of the door to open it
                DoorOpen_final = new Vector3(-length, 0, -length);
            }
            else if (TheRotationBase == -90)
            {
                length = size.z / 2;
                //The path of the door to open it
                DoorOpen_final = new Vector3(-length, 0, length);
            }

            //The position of the door at the beginning
            DoorOpen = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

            // load audio
            audioSources = this.GetComponents<AudioSource>();
            Doorgrinch = audioSources[0];
            DoorClick = audioSources[1];
        }

        //If we haven't open the door, we must to continue
        if (n_step < counter)
        {
            //Add a step to the angle of the door
            doorAngleStep = doorAngleStep + (doorOpenAngle / counter);
            if (TheRotationBase == 0)
            {
                targetRotation_step = Quaternion.Euler(0, doorAngleStep, 0);
            }
            else if (TheRotationBase == 90)
            {
                targetRotation_step = Quaternion.Euler(0, TheRotationBase + doorAngleStep, 0);
            }
            else if (TheRotationBase == 180)
            {
                targetRotation_step = Quaternion.Euler(0, TheRotationBase + doorAngleStep, 0);
            }
            else if (TheRotationBase == -90)
            {
                targetRotation_step = Quaternion.Euler(0, TheRotationBase + doorAngleStep, 0);
            }

            //Add a step to the translation of the door
            DoorOpen = DoorOpen + DoorOpen_final / counter;
            //Turn and translate the door of a little step
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation_step, rotationSpeed * Time.deltaTime);
            this.transform.position = Vector3.MoveTowards(this.transform.position, DoorOpen, moveSpeed * Time.deltaTime);
            


            //Tell to the program that we have done a step more
            n_step++;
            //Play the music of the door if this is the first time
            if (play_door)
            {
                DoorClick.Play();
                Doorgrinch.Play();
                play_door = false;
            }
            //we must to continue to open the door
        } else
        {
            door_not_open = false;
            boxCollider.transform.position = this.transform.position;
            boxCollider.transform.rotation = this.transform.rotation;
            //boxCollider.size = new Vector3(Size_Z, Size_Y, Size_X);
            //boxCollider.center = this.transform.localPosition;
        }
        //The door is now open, we can stop
        return door_not_open;
    }
}
