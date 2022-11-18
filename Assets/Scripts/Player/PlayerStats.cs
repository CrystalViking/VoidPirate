using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : ActorStats
{
    private HudScript hud;
    StatModManager statModManager;

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
    public void SpeedBuffSecondsPercentAdd(StatModApplicationType statModApplicationType, float value, float seconds)
    {
        StatModApplier statModApplier = new StatModApplier();
        statModApplier.SetModifier(value, StatModType.PercentAdd, ref statSpeed, statModApplicationType);
        statModManager.AddApplier(statModApplier, seconds);
    }

    public void SpeedDebuffSecondsPercentAdd(StatModApplicationType statModApplicationType, float value, float seconds)
    {
        value = -value;

        StatModApplier statModApplier = new StatModApplier();
        statModApplier.SetModifier(value, StatModType.PercentAdd, ref statSpeed, statModApplicationType);
        statModManager.AddApplier(statModApplier, seconds);
    }


    public override void Die()
    {
        base.Die();
        SceneManager.LoadScene("GameOverScene");
    }

    private void Update()
    {
        speed = statSpeed.Value;
    }

    public override void InitVariables(float maxHealth = 100)
    {
        base.InitVariables(maxHealth);

        statModManager = GetComponent<StatModManager>();
    }




}
