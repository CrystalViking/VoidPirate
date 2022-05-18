using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : ActorStats
{
    private HudScript hud;

    private void Start()
    {
        GetReferences(); 
        InitVariables();
    }

    private void GetReferences()
    {
        hud = GetComponent<HudScript>();
    }

    public override void CheckHealth()
    {
        base.CheckHealth();
        hud.UpdateHealth(health, maxHealth);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(10);
        }
    }






}
