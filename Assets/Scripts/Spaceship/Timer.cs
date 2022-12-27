using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Timer : SingletonMonobehaviour<Timer>

{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timeText;
    public bool isOxygenShortage = false;
    public bool isEnergyShortage = false;

    private void Start()
    {
        timerIsRunning = true;

        int number = Random.Range(0, 2);

        if (number == 0)
        {
            isOxygenShortage = true;
        }
        else
        {
            isEnergyShortage = true;
        }
    }
    void Update()
    {
        if (isOxygenShortage)
        {
            ProcessOxygenShortageEvent();
        }
        else if (isEnergyShortage)
        {
            ProcessEnergyShortageEvent();
        }
    }

    void ProcessOxygenShortageEvent()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                DisplayEventMessage("Critically low oxygen level");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }

    void ProcessEnergyShortageEvent()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                DisplayEventMessage("Critically low energy level");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void DisplayEventMessage(string message)
    {
        timeText.text = message;
    }
}