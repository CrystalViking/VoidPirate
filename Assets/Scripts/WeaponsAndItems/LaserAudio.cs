using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAudio : MonoBehaviour
{
    public AudioClip trigger;
    public AudioClip beamStart;
    public AudioClip beamLoop;
    public AudioClip beamStop;

    private AudioSource weaponAudioSource;

    private LaserShooting laserShooting;
    
    void Start()
    {
        weaponAudioSource = GetComponent<AudioSource>();
        GetReferences();
    }

    private void OnDisable()
    {
        //weaponAudioSource.Stop();
    }


    void Update()
    {
        
    }

    private void OnEnable()
    {
        weaponAudioSource.Stop();
    }

    private void TriggerProcAudio(object sender, EventArgs e)
    {
        weaponAudioSource.clip = trigger;
        if(!weaponAudioSource.isPlaying)
            weaponAudioSource.Play();
    }

    IEnumerator waitThenTurnOff(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        weaponAudioSource.Stop();
    }

    

    private void StartBeamAudio(object sender, EventArgs e)
    {
        //weaponAudioSource.Stop();
        weaponAudioSource.clip = beamStart;
        weaponAudioSource.Play();
    }

    private void HoldBeamAudio(object sender, EventArgs e)
    {
        weaponAudioSource.clip = beamLoop;
        weaponAudioSource.loop = true;
        if(!weaponAudioSource.isPlaying)
            weaponAudioSource.Play();
    }


    private void FinishBeamAudio(object sender, EventArgs e)
    {
        weaponAudioSource.Stop();
        weaponAudioSource.loop = false;
        weaponAudioSource.clip = beamStop;
        weaponAudioSource.Play();
    }

    private void GetReferences()
    {
        laserShooting = GetComponent<LaserShooting>();
        laserShooting.TriggerProcAudio += TriggerProcAudio;
        laserShooting.StartBeamAudio += StartBeamAudio;
        laserShooting.HoldBeamAudio += HoldBeamAudio;
        laserShooting.FinishBeamAudio += FinishBeamAudio;
    }
}
