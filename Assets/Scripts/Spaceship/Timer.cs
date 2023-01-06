using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class Timer : SingletonMonobehaviour<Timer>

{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public bool didEnergyEventSucceed = false;
    public bool didOxygenEventSucceed = false;
    public Text timeText;
    public bool isOxygenShortage = false;
    public bool isOxygenStationInGoodCondition;

    public bool isEnergyShortage = false;
    public float lightLevel = 1f;

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
            lightLevel = 0.65f;
        }

        int number1 = Random.Range(0, 2);
        isOxygenStationInGoodCondition = number1 == 0;
    }

    void Update()
    {
        if (isEnergyShortage)
        {
            ProcessEnergyShortageEvent();
        }
        else if (isOxygenShortage)
        {
            ProcessOxygenShortageEvent();
        }

        GameResources.Instance.litMaterial.SetFloat("Alpha_Slider", lightLevel);
    }

    void ProcessOxygenShortageEvent()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining, "Oxygen shortage");
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
                DisplayTime(timeRemaining, "Energy shortage");
            }
            else
            {
                Debug.Log("Time has run out!");
                DisplayEventMessage("Critically low energy level");
                timeRemaining = 0;
                timerIsRunning = false;
            }
        }
        if ((didEnergyEventSucceed && isOxygenStationInGoodCondition) || (didEnergyEventSucceed && didOxygenEventSucceed))
        {
            timerIsRunning = false;
            lightLevel = 1f;
            DisplayEventMessage("Event succeeded");
        }
    }
    void DisplayTime(float timeToDisplay, string eventName = "")
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = eventName + " " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void DisplayEventMessage(string message)
    {
        timeText.text = message;
    }
}