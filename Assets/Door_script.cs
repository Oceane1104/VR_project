using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;


public class Door_script : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioSource Doorgrinch; // make noise if the door open
    public AudioSource DoorClick; // make noise if opemn door


    void plax_door()
    {
        Doorgrinch.Play();
        DoorClick.Play();
    }
}
