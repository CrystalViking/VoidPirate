using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunReloading : MonoBehaviour
{
    [SerializeField]
    bool isReloading = false;
    [SerializeField]
    bool reloadFinished = false; 
    [SerializeField]
    int phaseFlag = 0;

    [SerializeField]
    bool reloadInterrupted = false;

    [SerializeField]
    float phaseSeconds;

    float time;
    private AudioSource weaponAudioSource;


    public event EventHandler ReloadFinished;



    bool reloadPhasePlaying = false;
    

    private void Start()
    {
        weaponAudioSource = GetComponent<AudioSource>();
        isReloading = false;
    }

    private void Update()
    {
        time = Time.time;
    }

    private void OnDisable()
    {
        if (isReloading)
            reloadInterrupted = true;

    }

    private void OnEnable()
    {
        if (isReloading)
        {
            reloadInterrupted = true;
            reloadPhasePlaying = false;
            weaponAudioSource.Stop();
        }
            
    }

    public bool isFinished()
    {
        return reloadFinished;
    }

    public bool isInterrupted()
    {
        return reloadInterrupted;
    }

    public void Reload(WeaponSO weaponData)
    {
        //float phaseSeconds;
        if(reloadInterrupted == false)
        {
            isReloading = true;
            reloadFinished = false;
        }        
        else
        {
            isReloading = true;
            reloadFinished = false;
            reloadInterrupted = false;
        }

        if (weaponData.reloadPhases.Count > 0 && weaponData.reloadTime > 0)
        {
            if(reloadInterrupted == false)
                phaseSeconds = weaponData.reloadTime / weaponData.reloadPhases.Count;
           

            if(isReloading)
                StartCoroutine(reloadPhase(weaponData, phaseSeconds));
           
           
            

        }
    }

    IEnumerator reloadPhase(WeaponSO weaponData, float phaseDuration)
    {
        while (phaseFlag < weaponData.reloadPhases.Count)
        {
            if (reloadPhasePlaying == false)
            {
                reloadPhasePlaying = true;
                weaponAudioSource.clip = weaponData.reloadPhases[phaseFlag];
                weaponAudioSource.Play();
                yield return new WaitForSeconds(phaseDuration);
                phaseFlag++;
                reloadPhasePlaying = false;
            }
        }
        if(phaseFlag == weaponData.reloadPhases.Count)
        {
            reloadFinished = true;
            reloadInterrupted = false;
            isReloading = false;
            phaseFlag = 0;
            phaseSeconds = 0;
            ReloadFinished?.Invoke(this, EventArgs.Empty);
            yield return null;
        }
    }

    public bool IsReloading()
    {
        return isReloading;
    }
}
