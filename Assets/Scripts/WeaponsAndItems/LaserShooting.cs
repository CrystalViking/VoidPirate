using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShooting : GunShooting
{

    public LineRenderer laser;
    public Transform firePoint;

    public GameObject startVFX;
    public GameObject endVFX;


    public event EventHandler TriggerProcAudio;
    public event EventHandler StartBeamAudio;
    public event EventHandler HoldBeamAudio;
    public event EventHandler FinishBeamAudio;

    bool laserCycleFinished = true;

    private LaserBeam laserBeam;

    private List<ParticleSystem> particles = new List<ParticleSystem>();

    private void OnDisable()
    {
        
    }

    private void OnEnable()
    {
        for (int i = 0; i < particles.Count; i++)
        {
            if (particles[i].isPlaying)
                particles[i].Stop();
        }
        laser.enabled = false;
    }

    private void Start()
    {
        GetReferences();
        FillLists();
        DisableLaserAtStart();

        
    }

    private void FillLists()
    {
        for(int i = 0; i < startVFX.transform.childCount; i++)
        {
            var ps = startVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if(ps != null)
                particles.Add(ps);
        }

        for (int i = 0; i < endVFX.transform.childCount; i++)
        {
            var ps = endVFX.transform.GetChild(i).GetComponent<ParticleSystem>();
            if (ps != null)
                particles.Add(ps);
        }
    }

    private void Update()
    {
        TakeInput();   
    }

    private void LateUpdate()
    {
        UpdateLaser();
    }

    protected override void TakeInput()
    {
        if (IsActive())
        {
            if (weaponData != null && !isReloading) // TODO: add weaponSO
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {
                    Shoot();
                }

                if (Input.GetKeyUp(KeyCode.Mouse0))
                {
                    DisableLaser();
                }
            }

            if (isReloading == false || reloadingInterrupted == true)
            {
                if (Input.GetKeyDown(KeyCode.R))
                {
                    Reload();
                }
            }
        }
    }




    protected override void Shoot()
    {
        StartCoroutine(LaserDelay(0.5f));
         
        //EnableLaser();

    }

    IEnumerator LaserDelay(float seconds)
    {
        if(laserCycleFinished)
        {
            laserCycleFinished = false;
            TriggerProcAudio?.Invoke(this, EventArgs.Empty);
            yield return new WaitForSeconds(seconds);
            //StartBeamAudio?.Invoke(this, EventArgs.Empty);
            //yield return new WaitForSeconds(1f);
            EnableLaser();
        }
        
        

    }

    void EnableLaser()
    {
        laser.enabled = true;
        HoldBeamAudio?.Invoke(this, EventArgs.Empty);
        for(int i = 0; i< particles.Count; i++)
        {
            if(!particles[i].isPlaying)
                particles[i].Play();
        }
    }

    void UpdateLaser()
    {
 
        laser.SetPosition(0, firePoint.position);
        startVFX.transform.position = (Vector2)firePoint.position;

        RaycastHit2D hit = Physics2D.Raycast(firePoint.position, transform.right);
        
        

        
        if (hit.collider)
        {
            laser.SetPosition(1, hit.point);
        }
        else
        {
            laser.SetPosition(1, firePoint.right * 100f);
        }


        endVFX.transform.position = laser.GetPosition(1);
        
        if(laser.enabled == true)
        {
            laserBeam.DealDamage(hit.collider);
        }
    }

    void DisableLaser()
    {
        //laserCycleFinished = true;
        StartCoroutine(TurnOffDelay());
        //laser.enabled = false;
        //FinishBeamAudio?.Invoke(this, EventArgs.Empty);
        //for (int i = 0; i < particles.Count; i++)
        //{
        //    if (particles[i].isPlaying)
        //        particles[i].Stop();
        //}
    }

    void DisableLaserAtStart()
    {
        laser.enabled = false;
        //FinishBeamAudio?.Invoke(this, EventArgs.Empty);
        for (int i = 0; i < particles.Count; i++)
        {
            if (particles[i].isPlaying)
                particles[i].Stop();
        }
    }

    IEnumerator TurnOffDelay()
    {
        yield return new WaitForSeconds(0.5f);
        laserCycleFinished = true;
        laser.enabled = false;
        FinishBeamAudio?.Invoke(this, EventArgs.Empty);
        for (int i = 0; i < particles.Count; i++)
        {
            if (particles[i].isPlaying)
                particles[i].Stop();
        }
    }

    private void GetReferences()
    {
        laserBeam = GetComponent<LaserBeam>();
    }
}
