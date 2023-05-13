using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_background : MonoBehaviour
{
    public AudioClip backgroundMusic;
    public AudioClip horrorSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();

        InvokeRepeating("PlayHorrorSound", 120.0f, 120.0f);
    }

    private void PlayHorrorSound()
    {
        audioSource.PlayOneShot(horrorSound);
    }
    //public AudioClip backgroundMusic;
    //public AudioClip horrorSound;

    //private AudioSource audioSource;

    //private void Start()
    //{
    //    AudioSource[] All_audio = this.GetComponents<AudioSource>();
    //    audioSource = All_audio[0];
    //    horrorSound = All_audio[1];
    //    audioSource.clip = backgroundMusic;
    //    audioSource.loop = true;
    //    audioSource.Play();

    //    InvokeRepeating("PlayHorrorSound", 120.0f, 120.0f);
    //}

    //private void PlayHorrorSound()
    //{
    //    audioSource.PlayOneShot(horrorSound);
    //}
}
