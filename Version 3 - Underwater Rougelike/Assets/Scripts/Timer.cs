using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private bool timerActive;
    public float currentTime;
    public float bestTime = 0f;
    [SerializeField] private TMP_Text text;

    private float topTime;

    [SerializeField] public Player player;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        StartTimer();

        // Ensure player and text references are assigned
        if (player == null)
        {
            Debug.LogError("Player reference is not set in the Timer script.");
        }

        if (text == null)
        {
            Debug.LogError("TMP_Text reference is not set in the Timer script.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(timerActive)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        if(text != null)
        {
            TimeSpan time = TimeSpan.FromSeconds(currentTime);
            text.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
        }
    
        if(player != null && !player.isAlive)
        {
            StopTimer();
        }
    }

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
}
