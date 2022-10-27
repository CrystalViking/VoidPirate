using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStats : MonoBehaviour
{
    [SerializeField] protected float health; // aktualne punkty zdrowia w postaci float
    [SerializeField] protected float maxHealth; // maksymalna liczba punktow zdrowia
    protected CharacterStat StatHealth; // punkty zdrowia w postaci CharacterStat (dla kontroli modyfikatorow)
    [SerializeField] protected float speed;
    protected CharacterStat statSpeed;

    protected StatModifier speedStatModifier;

    private bool timerIsRunning;
    private bool speedModifierOn;

    protected bool isDead;

    private void Start()
    {
        InitVariables();
    }

    public virtual void CheckHealth()
    {
        if (health <= 0f)
        {
            health = 0f;
            Die();
        }
        if (health >= maxHealth)
        {
            health = maxHealth;
        }
    }

   

    public float GetSpeed()
    {
        return speed;
    }

    public float GetHealth()
    {
        return health;
    }

    public virtual void Die()
    {
        isDead = true;
    }

    public void SetHealthTo( float healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    public void SetStatSpeed(float speed)
    {
        statSpeed.BaseValue = speed;
    }

    private void ModifySpeedPercentAdd(float percantage)
    {
        speedStatModifier = new StatModifier(percantage, StatModType.PercentAdd);
        statSpeed.AddModifier(speedStatModifier);
        speed = statSpeed.Value;
    }

    private void RemoveModifySpeedPercentAdd()
    {
        statSpeed.RemoveModifier(speedStatModifier);
        speed = statSpeed.Value;
    }


    public void ModifySpeedForTimeSeconds(float seconds)
    {
        speedModifierOn = true;
    }

    public void TakeDamage(float damage)
    {
        float healthAfterDamage = health - damage;
        SetHealthTo(healthAfterDamage);
    }

    public void Heal(float heal)
    {
        float healthAfterHeal = health + heal;
        SetHealthTo(healthAfterHeal);
    }

    public virtual void InitVariables(float maxHealth = 100)
    {
        SetHealthTo(maxHealth);
        SetStatSpeed(speed);
        isDead = false;
        timerIsRunning = true;
        speedModifierOn = false;
    }
}
