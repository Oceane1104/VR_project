using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;

public class Timer : MonoBehaviour
{
    public float timerDuration = 900.0f; // 15 minutes in seconds
    public Canvas gameOverCanvas;
    public Canvas winCanvas;
    public TextMeshProUGUI timerText;
    public AudioClip gameOverSound;
    public AudioClip winSound;

    public bool game_start = false;

    private AudioSource audioSource;
    public bool gameFinished = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void StartTimer()
    {
        Invoke("GameOver", timerDuration);
        game_start = true;
        //gameOverCanvas.SetActive(false);
        //winCanvas.SetActive(false);
    }

    private void Update()
    {
        if (gameFinished)
            return;
        if (game_start)
        {
            float remainingTime = Mathf.Max(0f, timerDuration - Time.timeSinceLevelLoad);
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60f);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            if((minutes == 0) && (seconds == 0))
            {
                GameOver();
            }
        }
    }

    public void testCanva()
    {
        gameOverCanvas.enabled = true;
    }

    private void GameOver()
    {
        //gameOverCanvas.SetActive(true);
        gameOverCanvas.enabled = true;
        audioSource.PlayOneShot(gameOverSound);
        gameFinished = true;
    }

    public void RestartGame()
    {
        if (gameFinished)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
        }
    }

    public void WinGame()
    {
        //winCanvas.SetActive(true);
        winCanvas.enabled = false;
        audioSource.PlayOneShot(winSound);
    }
}
