using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Hand : MonoBehaviour
{
    public Climber climber = null;
    public OVRInput.Controller controller = OVRInput.Controller.None;
    public Vector3 Delta { private set; get; } = Vector3.zero;

    private Vector3 lastPosition = Vector3.zero;
    private GameObject currentPoint = null;
    public List<GameObject> contactPoints = new List<GameObject>();
    private MeshRenderer meshRenderer = null;


    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    private void Start()
    {
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            Debug.Log("Button activated ");
            GrabPoint();
        }


        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, controller))
        {
            Debug.Log("Button desactivated ");
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
    private void GrabPoint()
    {
        currentPoint = Utility.GetNearest(transform.position, contactPoints);
        if (currentPoint)
        {
            //climber.SetHand(this);
            //meshRenderer.enabled = false;
        }

    }

    public void ReleasePoint()
    {
        if (currentPoint)
        {
            //climber.Clearhand();
            //meshRenderer.enabled = true;
        }
        currentPoint = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        AddPoint(other.gameObject);
        Debug.Log("Trigger activated ");
    }

    private void AddPoint(GameObject newObject)
    {
        if (newObject.CompareTag("ClimbPoint"))
        {
            contactPoints.Add(newObject);
        }
            
    }

    private void OnTriggerExit(Collider other)
    {
        RemovePoint(other.gameObject);
        Debug.Log("Trigger desactivated ");
    }

    private void RemovePoint(GameObject newObject)
    {
        if (newObject.CompareTag("ClimbPoint"))
        {
            contactPoints.Remove(newObject);
        }
    }

}
