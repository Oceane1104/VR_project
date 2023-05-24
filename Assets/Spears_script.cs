using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spears_script : MonoBehaviour
{
    public Vector3 init_pos;
    public float init_rot_x;
    public float init_rot_y;
    public float init_rot_z;
    public float HC_rot_x;
    public float HC_rot_y;
    public float HC_rot_z;
    public float spearsAngle = 0f; // the angle to open the door
    public Quaternion desiredRotation;
    public float rotationSpeed = 120;

    public AudioSource audioSources_throw;

    private Quaternion initialRotation;
    private Transform controllerTransform;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        init_pos = this.transform.position;
        init_rot_x = this.transform.rotation.eulerAngles.x;
        init_rot_y = this.transform.rotation.eulerAngles.y;
        init_rot_z = this.transform.rotation.eulerAngles.z;
        AudioSource[] audioSources_rb = this.GetComponents<AudioSource>();
        audioSources_throw = audioSources_rb[0];
        desiredRotation = Quaternion.Euler(0, 0, spearsAngle);
    }

    public void make_sound()
    {
        audioSources_throw.Play();
    }

    public void special_attach(HandController hand_controller)
    {
        Quaternion desiredRotation = Quaternion.Euler(0f, 0f, 90f) * Quaternion.Inverse(controllerTransform.rotation) * initialRotation;
        transform.rotation = controllerTransform.rotation * desiredRotation;
        //HC_rot_x = hand_controller.transform.rotation.eulerAngles.x;
        //HC_rot_y = hand_controller.transform.rotation.eulerAngles.y;
        //HC_rot_z = hand_controller.transform.rotation.eulerAngles.z;
        //this.transform.rotation = Quaternion.Euler(init_rot_x + HC_rot_x, init_rot_y + HC_rot_y + spearsAngle, init_rot_z + HC_rot_z);
        this.transform.rotation = Quaternion.RotateTowards(hand_controller.transform.rotation, desiredRotation, rotationSpeed * Time.deltaTime);
        this.transform.rotation = desiredRotation * this.transform.parent.rotation;

        controllerTransform.position = hand_controller.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
