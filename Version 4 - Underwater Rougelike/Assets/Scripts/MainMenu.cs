using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour
{
    // Main Menu Variables
    [SerializeField] private TMP_Text bestTimeText;
    [SerializeField] private TMP_Text currentTimeText;

    // Timer variables
    private bool timerActive;
    private float currentTime;
    private float bestTime = 0f;
    [SerializeField] private TMP_Text timerText;

    private float topTime;

    [SerializeField] public Player player;

    void Start()
    {
        currentTime = 0;
        StartTimer();
    }

    void Update()
    {
        if(timerActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        if(timerText != null)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            timerText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
        }
    
        if(player.isAlive == false) 
        {
            StopTimer();
        }
    }

    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has Quit");
    }

    // Timer 
    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        if(currentTime > bestTime)
        {
            bestTime = currentTime;
        }
        timerActive = false;
    }

    public string FormatTime(float timeInSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}:{2:D3}", time.Minutes, time.Seconds, time.Milliseconds);
    }

    public void DeathScreen()
    {

        bestTimeText.text = "Best Time: " + FormatTime(bestTime);
        currentTimeText.text = "Current Time: " + FormatTime(currentTime);
    }
}
