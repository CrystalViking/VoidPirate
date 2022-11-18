using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthModApplier : StatModApplier
{
    private bool poisonActive = false;
    private bool poisonCycleActive = false;
    private float durationSecondsLeft;
    private Task cycleTimer;

    private float hpDecrease;
    private float duration;
    private float cycleSeconds;
    
    private IEnumerator Poison(float hpDecrease, float duration, float cycleSeconds = 1f)
    {
        poisonActive = true;
        durationSecondsLeft = duration;

        while(poisonActive)
        {
            if(durationSecondsLeft < cycleSeconds || durationSecondsLeft < 0)
            {
                poisonActive = false;
            }
            else if(!poisonCycleActive)
            {
                poisonCycleActive = true;
                durationSecondsLeft -= cycleSeconds;
                characterStat.BaseValue -= hpDecrease;
                yield return new WaitForSeconds(cycleSeconds);
                poisonCycleActive = false;

            }
        }

        Expired = true;

    }

    public void SetPoisonModifier(float hpDecrease, ref CharacterStat characterStat, StatModApplicationType statModApplicationType)
    {
        this.characterStat = characterStat;
        ModApplicationType = statModApplicationType;

        this.hpDecrease = hpDecrease;       
        cycleSeconds = 1f;
    }
    public void SetPoisonModifier(float hpDecrease, float cycleSeconds, ref CharacterStat characterStat, StatModApplicationType statModApplicationType)
    {
        this.characterStat = characterStat;
        ModApplicationType = statModApplicationType;

        this.hpDecrease = hpDecrease;
        this.cycleSeconds = cycleSeconds;
    }

    public override void ApplyModifierForSeconds(float seconds)
    {
        duration = seconds;
        timer = new Task(Poison(hpDecrease, duration));
    }

    

}
