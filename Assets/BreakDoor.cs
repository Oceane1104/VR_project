
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDoor : MonoBehaviour
{
    public AudioSource[] audioSources; 
    public AudioSource HitSound; // make noise if hit w something
    public AudioSource ShatterSound; // make noise if shatter

    protected float Resistance = 30F; // "strength"/"XP"; goes to zero => break
    protected string Weakness = "blade";//"axe"; // thing it can be damaged by


    // Start is called before the first frame update
    void Start()
    {
        // load audio
        audioSources = GetComponents<AudioSource>();
        HitSound = audioSources[0];
        ShatterSound = audioSources[1];
    }

    // called by axe if hits door
    public int onBladeHit(GameObject child)
    {
        if (child.name == Weakness)
        {
            float vel = child.GetComponent<AxeCollider>().vel.magnitude;
            Resistance -= vel;

            Debug.LogWarningFormat("New resistance: {0}", Resistance);

            if (Resistance <= 0)
            {
                // door successfully broken!
                if (ShatterSound) ShatterSound.Play();
                Debug.LogWarningFormat("Destroying...");
                Destroy(this); // removed once call is done
                Destroy(gameObject);

                return 1;
            }
            else
            {
                if (HitSound) HitSound.Play();
                HitSound.Play(); // some damage => provide feedback
            }
        }
        return 0;
    }
    
    // call upon collision ie if hit by something
/*
    void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarningFormat("Collision! Resistance remaining: {0}", Resistance);
        GameObject child = collision.gameObject.transform.GetChild(1).gameObject; // get which part of object collided
        if (child == null) Debug.Log("Null :(");
        Debug.LogWarningFormat("Collider (child) name: {0}", child.name);
        float vel = child.GetComponent<AxeCollider>().vel.magnitude;
        Debug.LogWarningFormat("Velocity: {0}", vel);

        if (child.name == Weakness)
        {
            //float vel = collision.relativeVelocity.magnitude;
           // if (vel > 0.2) // if fast enough else probably no real force
            {
                // minus xp wrt velocity
                Resistance -= vel * 5;

                Debug.LogWarningFormat("New resistance: {0}", Resistance);

                if (Resistance <= 0)
                {
                    // door successfully broken!
                    if (ShatterSound) ShatterSound.Play();
                    Debug.LogWarningFormat("Destroying...");
                    Destroy(this); // removed once call is done
                    Destroy(gameObject);
                }
                else
                {
                    if (HitSound) HitSound.Play();
                    HitSound.Play(); // some damage => provide feedback
                }
            }
        }
        // not being hit with correct object => ignore
    }*/
}
