using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/*public class Hand : MonoBehaviour
{
    public Climber climber = null;
    public OVRInput.Controller controller = OVRInput.Controller.None;
    public LayerMask climbPointLayer;
    public float climbingSpeed = 2f;

    private bool isGrabbing = false;
    private Transform currentClimbPoint = null;

    private void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            GrabClimbPoint();
        }


        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            ReleaseClimbPoint();
        }
    }

    private void FixedUpdate()
    {
        if (isGrabbing && currentClimbPoint != null)
        {
            Climb();
        }
    }

    private void GrabClimbPoint()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, climbPointLayer))
        {
            currentClimbPoint = hit.transform;
            climber.SetHand(this);
            isGrabbing = true;
        }
    }

    public void ReleaseClimbPoint()
    {
        climber.ClearHand();
        currentClimbPoint = null;
        isGrabbing = false;
    }

    private void Climb()
    {
        //float verticalInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, controller).y;
        //float climbSpeed = verticalInput * climbingSpeed;
        climber.Climb(currentClimbPoint, climbingSpeed);
    }
}*/








public class Hand : MonoBehaviour
{
    public Climber climber = null;
    public OVRInput.Controller controller = OVRInput.Controller.None;
    public Vector3 middleposition;
    public Vector3 Delta { private set; get; } = Vector3.zero;

    private Vector3 lastPosition = Vector3.zero;
    private GameObject currentPoint = null;
    public List<GameObject> contactPoints = new List<GameObject>();
    //private bool BoolClimb = false;




    private void Awake()
    {
    }

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            GrabPoint();
        }


        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            //Debug.LogWarning("Button is released");
            ReleasePoint();
        }
    }

    private void fixedUpdate()
    {
        lastPosition = transform.position;
    }

    private void LateUpdate()
    {
        Delta = lastPosition - transform.position;
    }
    public void GrabPoint()
    {

        currentPoint = Utility.GetNearest(transform.position, contactPoints);
        //Debug.LogWarning("current" +currentPoint.transform.position);
        if (currentPoint)
        {
            //Debug.LogWarning("Grabpoint yes");
            climber.SetHand(this);
        }
    }

    public void ReleasePoint()
    {
        if (currentPoint)
        {
            //Debug.LogWarning("ReleasePoint yes");
            climber.ClearHand();
        }
        currentPoint = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ClimbPoint"))
        {
            //Debug.LogWarning("Trigger activated");
            //AddPoint(other.gameObject);
            contactPoints.Add(other.gameObject);
        }

    }



    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ClimbPoint"))
        {
            contactPoints.Remove(other.gameObject);

        }
        //Debug.LogWarning("Trigger desactivated");
    }


    public GameObject GetCurrentPoint()
    {
        if (currentPoint != null)
        {
            return currentPoint;
        }
        else
            return null;
    }
}


/*private void AddPoint(GameObject newObject)
{
    if (newObject.CompareTag("ClimbPoint"))
    {
        contactPoints.Add(newObject);
    }
}
private void RemovePoint(GameObject newObject)
{
    if (newObject.CompareTag("ClimbPoint"))
    {
        contactPoints.Remove(newObject);
    }
}

}*/
