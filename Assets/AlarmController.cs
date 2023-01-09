using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmController : MonoBehaviour
{
    private GameObject player;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public float volume;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        audioSource.volume = volume;
    }

    void Update()
    {
        if(player.GetComponent<PShipHealth>().GetHealth() < 150f)
        {
            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
        
    }
}
