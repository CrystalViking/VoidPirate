using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineCountDown : MonoBehaviour
{
    private bool timerStarted;
    private bool timerSet;
    private float timerSeconds;

    private void Start()
    {
        InitVariables();
    }

    void Update()
    {
        
    }


    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(timerSeconds);
        StopTimer();
    }

    public void SetTimer(float seconds)
    {
        if(seconds > 0f)
        {
            timerSeconds = seconds;
            timerSet = true;
        }
    }

    public void StartTimer()
    {
        if(timerSet)
        {
            timerStarted = true;
            StartCoroutine(CountDown());
        }
        
    }

    private void StopTimer()
    {
        if(timerSet)
        {
            timerStarted = false;
            timerSet = false;
        } 
    }

    private void InitVariables()
    {
        timerSeconds = 0f;
        timerStarted = false;
        timerSet = false;

    }
}
