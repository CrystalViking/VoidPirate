using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : ActorStats
{
    private HudScript hud;
    private Task speedModifier;

    private void Start()
    {
        GetReferences(); 
        InitVariables(maxHealth);
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

    public void ApplySpeedModifier(float seconds, float percentage)
    {
        speedModifier = new Task(ModifySpeedForTimeSeconds(seconds, percentage));   
        //speedModifier.Stop();
    }


    public override void Die()
    {
        base.Die();
        SceneManager.LoadScene("GameOverScene");
    }

    private void Update()
    {
        
    }






}
