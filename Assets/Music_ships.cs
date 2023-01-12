using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_ships : MonoBehaviour
{
    [Header("Music")]
    public AudioSource[] audioSources;
    public float volume = 0.06f;
    private int i;
    void Start()
    {
        i = Random.Range(0, audioSources.Length);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = volume;
        }

        audioSources[i].Play();
    }
}
