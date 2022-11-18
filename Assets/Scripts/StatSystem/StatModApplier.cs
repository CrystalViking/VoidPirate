using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatModApplier : IStatModApplier
{
    private Task timer;
    StatModifier statModifier;
    CharacterStat characterStat;
    public bool Expired { get; private set; } = false;
    public StatModApplicationType ModApplicationType { get; private set; }

    

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


    public void ApplyModifierForSeconds(float seconds)
    {
        timer = new Task(modDuration(seconds));
    }

    IEnumerator modDuration(float seconds)
    {
        ApplyModifier();
        yield return new WaitForSeconds(seconds);
        RemoveModifer();

        Expired = true;

        Debug.Log("Expired");
    }
}
