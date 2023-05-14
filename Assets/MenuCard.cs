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
        // turn off any text that isn't "universal"
        foreach (SpriteRenderer t in Texts)
        {
            string name = t.name;
            if (name.Contains("Hover"))
            {
                t.enabled = false;
            }
            else
            {
                foreach (string txt in VariableTxt)
                {
                    if (name.Equals(txt))
                    {
                        t.enabled = false;
                        break;
                    }
                }
            }
        }
        gameObject.active = active; // have it off by default
        Texts = FindObjectsOfType<SpriteRenderer>(); // all possible images on card
        allObj = FindObjectsOfType<GameObject>();
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
        GameObject.Find("GameOverTxt").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("LostTxt").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void displayWinner()
    {
        toggleActive(true);
        toggleBackground();
        gameObject.active = active;
        GameObject.Find("GameOverTxt").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("WinnerTxt").GetComponent<SpriteRenderer>().enabled = true;
    }

    public void toggleActive(bool act = false)
    {
        active = act;
        GetComponent<HandController>().activeMenu = act;
    }
}
