using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class for when game ends

public class EndGame : MonoBehaviour
{
    public AudioSource minotaur; // sound to play if captured
    public AudioSource winner; // sound to play if finised game & won 

    // Start is called before the first frame update
    void Start()
    {
        minotaur = GetComponent<AudioSource>(); 
        winner = GetComponent<AudioSource>();
    }

    // game ends & you lost because out of time
    public void outOfTime() {
        if (minotaur) minotaur.Play();
        
    }

    public void wonGame()
    {
        if (winner) winner.Play();
    }
}
