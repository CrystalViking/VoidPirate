using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Radio : MonoBehaviour
{
    public AudioMixer audioMixer;

    public List<RadioStation> radioStations;
    public int currentStation;

    public float waitBeforeOff = 300;
    // Start is called before the first frame update
    void Start()
    {
        //PlayRadio();
        currentStation = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AdjustRadioVolume(float value)
    {
        audioMixer.SetFloat("Volume", value);
    }

    public void StopRadio()
    {
        radioStations[currentStation].StopStation();   
    }

    public void PlayRadio()
    {
        for (int i = 0; i < radioStations.Count; i++)
        {
            if (i != currentStation)
            {
                radioStations[i].audioSource.volume = 0;
                radioStations[i].waitTime = waitBeforeOff;
                radioStations[i].currentRadio = false;
            }
        }
        radioStations[currentStation].audioSource.volume = 0.1f;
        radioStations[currentStation].audioSource.spatialBlend = 1;
        radioStations[currentStation].stopRadio = false;
        radioStations[currentStation].currentRadio = true;
    }

    public void NavigateStations(bool value)
    {
        if(value == true)
        {
            currentStation += 1;
            if(currentStation >= radioStations.Count)
            {
                currentStation = 0;
            }
        }
        else
        {
            currentStation -= 1;
            if(currentStation < 0)
            {
                currentStation = radioStations.Count - 1;
            }

        }
        PlayRadio();
    }
}
