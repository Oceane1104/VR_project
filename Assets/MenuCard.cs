using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// C# script for "menu" object

public class MenuCard : MonoBehaviour
{

    static SpriteRenderer[] Texts;
    static string[] VariableTxt = { "GameOverTxt", "PauseTxt", "WinnerTxt", "LostTxt" };
    static GameObject[] allObj;
    bool active = false;

    private HandController[] Hands;

    void Start()
    {
        disable();
        Texts = FindObjectsOfType<SpriteRenderer>(); // all possible images on card
        allObj = FindObjectsOfType<GameObject>();
        Hands = FindObjectsOfType<HandController>();
    }

    public void disable()
    {
        // make sure everything is disabled so not displayed on first render
        Component[] renderers = gameObject.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        gameObject.SetActive(false);
    }

    void enable()
    {
        Transform child1 = gameObject.transform.Find("QuitTxt");
        Transform child2 = gameObject.transform.Find("Restart");
        child1.GetComponent<SpriteRenderer>().enabled = true;
        child2.GetComponent<SpriteRenderer>().enabled = true;
    }

    void disEnable()
    {
        Transform child1 = gameObject.transform.Find("QuitTxt");
        Transform child2 = gameObject.transform.Find("Restart");
        child1.GetComponent<SpriteRenderer>().enabled = false;
        child2.GetComponent<SpriteRenderer>().enabled = false;
    }

    void toggleBackground()
    {
        foreach(GameObject g in allObj)
        {
            if (g == this) continue;
            g.SetActive(!active); // opposite to whatever menu is
        }
    }

    // display "fail" message
    public void displayFailure()
    {
        toggleActive(true);
        //toggleBackground();
        gameObject.SetActive(active);
        enable();
        GameObject.Find("GameOverTxt").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("LostTxt").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void displayWinner()
    {
        toggleActive(true);
        //toggleBackground();
        gameObject.SetActive(active);
        enable();
        GameObject.Find("GameOverTxt").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("WinnerTxt").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void Pause()
    {
        Debug.Log("Pausing");
        toggleActive(true);
        //toggleBackground();
        gameObject.SetActive(active);
        enable();
        GameObject.Find("PauseTxt").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Sounds").GetComponent<GameMusic>().setTicking(false); // pause timer
    }

    public void unPause()
    {
        //toggleBackground();
        disEnable();
        GameObject.Find("PauseTxt").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Sounds").GetComponent<GameMusic>().setTicking(true); // continue timer
        gameObject.SetActive(active);
        toggleActive(false);
    }

    public void toggleActive(bool act = false)
    {
        active = act;
        foreach (HandController hand in Hands)
        {
            hand.GetComponent<HandController>().activeMenu = act;
        }
        
    }
}
