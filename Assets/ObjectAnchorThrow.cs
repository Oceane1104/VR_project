using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class ObjectAnchorThrow : MonoBehaviour
{
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

    Quaternion targetRotation_step = Quaternion.Euler(0f, 0f, 0f);

    // Start is called before the first frame update
    void Start()
    {
        Vector3 size = lengthdoor.GetComponent<Renderer>().bounds.size;
        length = size.x / 2;

        //moveSpeed = -rotationSpeed * 2 * Mathf.PI * length * 2 / 360;

        //DoorOpen_final = new Vector3(door.transform.position.x + length, door.transform.position.y, door.transform.position.z - length);
        DoorOpen_final = new Vector3(length, 0, -length);
        DoorOpen = new Vector3(door.transform.position.x, door.transform.position.y, door.transform.position.z);
        //AngleDoorOpen = new Vector3(door.transform.rotation.x, door.transform.rotation.y + 90, door.transform.rotation.z);
    }

    protected void coll()
    {
        if (iscollision)
        {
            // rotate non smoothly the door
            //door.transform.position = Vector3.MoveTowards(door.transform.position, DoorOpen, moveSpeed * Time.deltaTime);
            //door.transform.rotation = Quaternion.RotateTowards(door.transform.rotation, Quaternion.Euler(AngleDoorOpen), rotationSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.Euler(0f, -doorOpenAngle, 0f);
            if (n_step < counter)
            {
                doorAngleStep = doorAngleStep + (doorOpenAngle / counter);
                targetRotation_step = Quaternion.Euler(0f, -doorAngleStep, 0f);
                DoorOpen = DoorOpen + DoorOpen_final / counter;
                door.transform.rotation = Quaternion.RotateTowards(door.transform.rotation, targetRotation_step, rotationSpeed * Time.deltaTime);
                door.transform.position = Vector3.MoveTowards(door.transform.position, DoorOpen, moveSpeed * Time.deltaTime);
                n_step++;
            }
            //door.transform.rotation = Quaternion.RotateTowards(door.transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            //door.transform.position = Vector3.MoveTowards(door.transform.position, DoorOpen, moveSpeed * Time.deltaTime);

            //Keep the collider on the target
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponent<Rigidbody>().transform.position = box.transform.position;
            //GetComponent<Rigidbody>().transform.SetParent(box.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        coll();
    }

    //Detect when the object collides with the target and open the door if it is the case
    private void OnCollisionEnter(Collision collision)
    { 
        if (collision.gameObject.layer == LayerMask.NameToLayer("TargetObject"))
        {
            iscollision = true;
        }
    }
}

