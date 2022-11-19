using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class RadioStation : MonoBehaviour
{


    [HideInInspector]
    public AudioSource audioSource;

    public List<AudioClip> stationClips;
    List<AudioClip> currentStationClips;

    public AudioClip currentClip;
    public int currentClipNumber;

    public bool currentRadio;

    public bool stopRadio;
    [HideInInspector]
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ShuffleRadio();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentRadio != true && stopRadio != true)
        {
            waitTime -= 1 * Time.deltaTime;
            if(waitTime <= 0)
            {
                audioSource.Stop();
                ShuffleRadio();
                stopRadio = true;
            }
        }

        if(stopRadio == false)
        {
            if(!audioSource.isPlaying)
            {
                
                if(currentClipNumber >= currentStationClips.Count)
                {
                    currentClipNumber = 0;
                    //ShuffleRadio();
                }
                else
                {
                    currentClipNumber++;
                }

                if(currentStationClips != null && (currentClipNumber >= 0) && (currentClipNumber < currentStationClips.Count))
                {
                    currentClip = currentStationClips[currentClipNumber];
                    audioSource.clip = currentClip;
                    audioSource.Play();
                }
                
                
            }
        }
    }

    public void StopStation()
    {
        audioSource.Stop();
        stopRadio = true;
    }




    public void ShuffleRadio()
    {
        currentStationClips = stationClips;
        List<int> numbersTaker = new List<int>();

        for (int i = 0; i < currentStationClips.Count; i++)
        {
            bool next = false;
            while (next != true)
            {
                int newNumber = Random.Range(0, stationClips.Count);
                if (numbersTaker.Contains(newNumber) != true)
                {
                    currentStationClips[i] = stationClips[newNumber];
                    next = true;
                }
            }
        }
    }
}
