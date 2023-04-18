using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDoor : MonoBehaviour
{
    public AudioSource HitSound; // make noise if hit w something
    public AudioSource ShatterSound; // make noise if shatter

    public float Resistance = 300; // "strength"/"XP"; goes to zero => break
    public string Weakness = "blade"; // thing it can be damaged by


    // Start is called before the first frame update
    void Start()
    {
        // load audio
        HitSound = GetComponent<AudioSource>();
        ShatterSound = GetComponent<AudioSource>();
    }

    // call upon collision ie if hit by something

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals(Weakness)) // make sure correct obj ex.  npt your hand
        {
            float vel = collision.relativeVelocity.magnitude;
            if (vel > 2) // if fast enough else probably no real force
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(255,0,0);
                // minus xp wrt velocity
                Resistance -= vel * 5;

                if (Resistance <= 0)
                {
                    // door successfully broken!
                    ShatterSound.Play();
                    Destroy(gameObject); // removed once call is done
                }
                else
                {
                    HitSound.Play(); // some damage => provide feedback
                }
            }
            else
            {
                gameObject.GetComponent<Renderer>().material.color = new Color(0, 255, 0);

            }
        }
        else
        {
            gameObject.GetComponent<Renderer>().material.color = new Color(0,0,255);

        }
        // not being hit with correct object => ignore
    }
}
