using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorStats : MonoBehaviour, IActorStats
{
    [SerializeField] protected float health; // aktualne punkty zdrowia w postaci float
    [SerializeField] protected float maxHealth; // maksymalna liczba punktow zdrowia
    protected CharacterStat StatHealth; // punkty zdrowia w postaci CharacterStat (dla kontroli modyfikatorow)
    [SerializeField] protected float speed;
    protected CharacterStat statSpeed;

    protected StatModifier speedStatModifier;

    //private CoroutineCountDown timer;

    //private bool timerIsRunning;
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

    public void SetHealthTo(float healthToSetTo)
    {
        health = healthToSetTo;
        CheckHealth();
    }

    public void SetStatSpeed(float speed)
    {
        if (statSpeed != null)
            statSpeed.BaseValue = speed;
        else
        {
            statSpeed = new CharacterStat();
            statSpeed.BaseValue = speed;
        }


    }


    // needs a rework
    private void ModifySpeedPercentAdd(float percentage)
    {
        if (!speedModifierOn)
        {
            speedStatModifier = new StatModifier(percentage, StatModType.PercentAdd);
            statSpeed?.AddModifier(speedStatModifier);
            speed = statSpeed.Value;
            speedModifierOn = true;

        }
    }

    // needs a rework
    private void RemoveModifySpeedPercentAdd()
    {
        if (speedModifierOn)
        {
            statSpeed.RemoveModifier(speedStatModifier);
            speed = statSpeed.Value;
            speedModifierOn = false;
        }
    }


    // needs a rework
    protected IEnumerator ModifySpeedForTimeSeconds(float seconds, float speedPercentage)
    {
        //speedModifierOn = true;
        ModifySpeedPercentAdd(speedPercentage);
        yield return new WaitForSeconds(seconds);
        RemoveModifySpeedPercentAdd();
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
        //timerIsRunning = true;
        speedModifierOn = false;
    }
}
