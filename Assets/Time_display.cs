//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;

//public class Time_display : MonoBehaviour
//{
//    public GameMusic gameMusic; // Reference to the GameMusic script (drag and drop the GameMusic script onto this field in the Inspector)

//    private TMP_Text timeText; // Reference to the Text component

//    public bool activeMenu = false; // if true instead of grabbing objects, hand used for quit, etc
//    public MenuCard menu;

//    private void Start()
//    {
//        timeText = GetComponent<TMP_Text>(); // Get the Text component attached to this GameObject
//        GetComponent<TMP_Text>().enabled = false;
//    }

//    private void Update()
//    {
//        if (gameMusic != null && gameMusic.IsTicking)
//        {
//            GetComponent<TMP_Text>().enabled = true;
//            float timeLeft = gameMusic.TimeLeft;

//            // Format the time as minutes and seconds
//            int minutes = Mathf.FloorToInt(timeLeft / 60);
//            int seconds = Mathf.FloorToInt(timeLeft % 60);

//            // Update the Text component with the formatted time
//            timeText.text = string.Format("Time: {0:00}:{1:00}", minutes, seconds);
//        }

//        if (activeMenu)
//        {
//            // menu open => wanna unpause
//            menu.unPause();
//        }
//        else
//        {
//            menu.Pause();
//        }
//    }
//}
