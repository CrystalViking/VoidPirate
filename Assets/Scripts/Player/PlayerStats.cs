using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerStats : ActorStats
{
    private HudScript hud;
    StatModManager statModManager;

    PlayerRenderer playerRenderer;

    public UnityEvent<float> OnHealthChange;

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
        hud.UpdateHealth(health);

        OnHealthChange?.Invoke((float)health/maxHealth);
    }
    public void SpeedBuffSecondsPercentAdd(StatModApplicationType statModApplicationType, float value, float seconds)
    {
        SpeedModApplier statModApplier = new SpeedModApplier();
        statModApplier.SetModifier(value, StatModType.PercentAdd, ref statSpeed, statModApplicationType);
        statModManager.AddApplier(statModApplier, seconds);
    }

    public void SpeedDebuffSecondsPercentAdd(StatModApplicationType statModApplicationType, float value, float seconds)
    {
        value = -value;

        SpeedModApplier statModApplier = new SpeedModApplier();
        statModApplier.SetModifier(value, StatModType.PercentAdd, ref statSpeed, statModApplicationType);
        statModManager.AddApplier(statModApplier, seconds);
    }

    public void HealthPoison(StatModApplicationType statModApplicationType, float hpDecrease, float duration)
    {
        HealthModApplier healthModApplier = new HealthModApplier();
        healthModApplier.SetPoisonModifier(hpDecrease, ref statHealth, statModApplicationType);
        statModManager.AddApplier(healthModApplier, duration);

        StartCoroutine(playerRenderer.FlashRed((int)duration));

    }

    


    public override void Die()
    {
        base.Die();
        SceneManager.LoadScene("GameOverScene");
    }

    private void Update()
    {
        try
        {
            speed = statSpeed.Value;
            SetHealthTo(statHealth.Value);
        }
        catch { }
    }

    public override void InitVariables(float maxHealth = 100)
    {
        base.InitVariables(maxHealth);

        statModManager = GetComponent<StatModManager>();
        playerRenderer = GetComponent<PlayerRenderer>();
    }




}
