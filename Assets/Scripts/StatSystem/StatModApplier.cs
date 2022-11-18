using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModApplier : IStatModApplier
{
    protected Task timer;
    protected StatModifier statModifier;
    protected CharacterStat characterStat;
    public bool Expired { get; protected set; } = false;
    public StatModApplicationType ModApplicationType { get; protected set; }

    

    public void SetModifier(float value, StatModType statModType, ref CharacterStat characterStat, StatModApplicationType statModApplicationType)
    {
        statModifier = new StatModifier(value, statModType);
        this.characterStat = characterStat;
        ModApplicationType = statModApplicationType;
    }

    public void ApplyModifier()
    {
        characterStat?.AddModifier(statModifier);
    }


    public void RemoveModifer()
    {
        characterStat?.RemoveModifier(statModifier);
    }


    public virtual void ApplyModifierForSeconds(float seconds)
    {
        timer = new Task(modDuration(seconds));
    }


    protected IEnumerator modDuration(float seconds)
    {
        ApplyModifier();
        yield return new WaitForSeconds(seconds);
        RemoveModifer();

        Expired = true;
    }
}
