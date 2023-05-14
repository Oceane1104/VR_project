using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// class to deal with music & all other background noise
// idea: have overarching music throughout game
//       on occasion have other sounds play
//       as more time passes, sound gets faster/more intense
// if run out of time play monster sound & terminate game (by calling other script)

public class GameMusic : MonoBehaviour
{
    // audiosource vary
    public AudioSource[] backgroundSounds; // ex. screams, howls; only plays sometimes
    public AudioSource gameMusic; // play throughout; EndGame script is one to stop it 
    private static int len;

    public static double prob = 0.5f; // "probability" of playing sound; play if rand # gen > prob
    private System.Random rnd = new System.Random(); // use this to generate random numbers

    private float timeLeft = 10f; // minutes for game
    private bool ticking = false; // must be set to true by another script
    // after tutorial ends: set to true
    // if pause is pressed, etc: set to false & reset to true when you keep going
    
    void setTicking(bool val) { ticking = val; }

    // add/remove time; use function sparingly
    void addTime(float time) { timeLeft += time; }

    // Start is called before the first frame update
    void Start()
    {
        backgroundSounds = GetComponents<AudioSource>();
        len = backgroundSounds.Length;
        gameMusic = GetComponent<AudioSource>();
        gameMusic.loop = true;

        gameMusic.Play();

    }

    void StopMusic() { if (gameMusic) gameMusic.Stop(); }

    // Update is called once per frame
    // use this to update time
    void Update()
    {
        if (ticking) { timeLeft -= Time.deltaTime; }

        if (timeLeft <= 0) { GetComponent<EndGame>().outOfTime(); }

        double r = rnd.NextDouble();
        if (r > prob)
        {
            // play sound; choose random one from list
            if (len == 0) return; // nothing to play :(

            int i = rnd.Next(0, len);
            backgroundSounds[i].Play();
        }
    }
}