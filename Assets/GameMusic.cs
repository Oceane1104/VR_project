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
    private AudioSource[] backgroundSounds; // ex. screams, howls; only plays sometimes
    private AudioSource gameMusic; // play throughout; EndGame script is one to stop it 
    private static int len; // number of sounds 

    public static double prob = 0.5f; // "probability" of playing sound; play if rand # gen > prob
    private System.Random rnd = new System.Random(); // use this to generate random numbers
    private float soundPause = 10f; // min second between sounds 

    private float timeLeft = 60f;//750f; // minutes for game
    private bool ticking = false; // must be set to when end tutorial
    // after tutorial ends: set to true
    // if pause is pressed, etc: set to false & reset to true when you keep going
    
    public void setTicking(bool val) { ticking = val; }

    // add/remove time; use function sparingly
    void addTime(float time) { timeLeft += time; }

    // Start is called before the first frame update
    void Start()
    {
        //backgroundSounds = GetComponents<AudioSource>();

        backgroundSounds = gameObject.GetComponentsInChildren<AudioSource>(); // remember to ignore first one
        len = backgroundSounds.Length;

        gameMusic = backgroundSounds[0];
        gameMusic.loop = true;
        gameMusic.Play();
    }

    void StopMusic() { if (gameMusic) gameMusic.Stop(); }

    // Update is called once per frame
    // use this to update time
    void Update()
    {
        if (ticking) { timeLeft -= Time.deltaTime; soundPause -= Time.deltaTime; }

        if (timeLeft <= 0) { setTicking(false);  GetComponent<EndGame>().outOfTime(); return; }

        double r = rnd.NextDouble();
        if (r > prob && soundPause <= 0)
        {
            soundPause = 10f;
            // play sound; choose random one from list
            if (len == 0) return; // nothing to play :(

            int i = rnd.Next(3, len);
            backgroundSounds[i].Play();
        }
    }
}
