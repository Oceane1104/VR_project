using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        GameObject.Find("Timer").GetComponent<TMP_Text>().enabled = false;

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
        float time = GameObject.Find("Sounds").GetComponent<GameMusic>().getTimeLeft();
        Debug.LogWarningFormat("Pausing. Time left: {0}", time);
        toggleActive(true);
        //toggleBackground();
        gameObject.SetActive(active);
        enable();
        /*TimeSpan Time = TimeSpan.FromSeconds(time);
        GameObject.Find("Timer").GetComponent<TMP_Text>().text = Time.ToString("mm\\:ss");
        GameObject.Find("Timer").GetComponent<TMP_Text>().enabled = true;
        GameObject.Find("Timer").SetActive(true);*/
        GameObject.Find("PauseTxt").GetComponent<SpriteRenderer>().enabled = true;
        GameObject.Find("Sounds").GetComponent<GameMusic>().setTicking(false); // pause timer
    }

    public void unPause()
    {
        //toggleBackground();
        disEnable();
        //GameObject.Find("Timer").GetComponent<TMP_Text>().enabled = false;
        GameObject.Find("PauseTxt").GetComponent<SpriteRenderer>().enabled = false;
        GameObject.Find("Sounds").GetComponent<GameMusic>().setTicking(true); // continue timer
        //GameObject.Find("Timer").SetActive(false);
        gameObject.SetActive(active);
        toggleActive(false);
        Debug.LogWarningFormat("Un-pause. Time left: {0}", GameObject.Find("Sounds").GetComponent<GameMusic>().getTimeLeft());
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
