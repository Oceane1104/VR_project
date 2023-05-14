using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// class for when game ends

public class EndGame : MonoBehaviour
{
    public AudioSource minotaur; // sound to play if captured
    public AudioSource winner; // sound to play if finised game & won 
    public MenuCard menu;

    // Start is called before the first frame update
    void Start()
    {
        minotaur = GetComponent<AudioSource>();
        winner = GetComponent<AudioSource>();

    }

    // game ends & you lost because out of time
    public void outOfTime() {
        menu.displayFailure();
        
        if (minotaur) minotaur.Play();
    }

    // won game bc made it to end 
    public void wonGame()
    {
        menu.displayWinner();
        
        if (winner) winner.Play();
    }

   public void quitGame()
    {
        Debug.Log("Quitting game. Goodbye...");
        Application.Quit();
    }

    public void restartGame()
    {
        Debug.Log("Restarting. Reload Scene...");
        menu.toggleActive(); // make sure menu inactive
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
