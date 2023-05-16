using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// class for when game ends

public class EndGame : MonoBehaviour
{
    private AudioSource minotaur; // sound to play if captured
    private AudioSource winner; // sound to play if finised game & won 
    public MenuCard menu;
    private HandController[] Hands;

    // Start is called before the first frame update
    void Start()
    {
        minotaur = gameObject.transform.GetChild(1).GetComponent<AudioSource>();
        winner = gameObject.transform.GetChild(2).GetComponent<AudioSource>();
        Hands = FindObjectsOfType<HandController>();
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
        GameObject.Find("Sounds").GetComponent<GameMusic>().setTicking(false);
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
        menu.disable();
        foreach (HandController hand in Hands)
        {
            hand.GetComponent<HandController>().cleanup();
        }
        GameObject.Find("Sounds").GetComponent<GameMusic>().setTicking(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
