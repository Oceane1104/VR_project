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

    void Start()
    {
        disable();
        Texts = FindObjectsOfType<SpriteRenderer>(); // all possible images on card
        allObj = FindObjectsOfType<GameObject>();
    }

    public void disable()
    {
        // make sure everything is disabled so not displayed on first render
        Component[] renderers = gameObject.GetComponentsInChildren(typeof(Renderer));
        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        gameObject.active = false; // have it off by default
    }

    void enable()
    {
        Transform child1 = gameObject.transform.Find("QuitTxt");
        Transform child2 = gameObject.transform.Find("Restart");
        child1.GetComponent<SpriteRenderer>().enabled = true;
        child2.GetComponent<SpriteRenderer>().enabled = true;
    }

    void toggleBackground()
    {
        foreach(GameObject g in allObj)
        {
            if (g == this) continue;
            g.active = !active; // opposite to whatever menu is
        }
    }

    // display "fail" message
    public void displayFailure()
    {
        toggleActive(true);
        toggleBackground();
        gameObject.active = active;
        enable();
        GameObject.Find("GameOverTxt").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("LostTxt").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void displayWinner()
    {
        toggleActive(true);
        toggleBackground();
        gameObject.active = active;
        enable();
        GameObject.Find("GameOverTxt").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("WinnerTxt").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void toggleActive(bool act = false)
    {
        active = act;
        GetComponent<HandController>().activeMenu = act;
    }
}
