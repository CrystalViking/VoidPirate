using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModApplier : MonoBehaviour, IStatModApplier
{
    private Task timer;
    StatModifier speedModifier;
    CharacterStat characterStat;
    public bool Expired { get; private set; }
    public StatModApplicationType ModApplicationType { get; private set; }

    private void Start()
    {
        //List<SpeedModHelper> speedMods = new List<SpeedModHelper>();
    }

    public void SetUpApplier(float value, StatModType statModType = StatModType.PercentAdd, ref CharacterStat characterStat, StatModApplicationType modApplicationType)
    {
        speedModifier = new StatModifier(value, statModType);
        statModApplicationType = modApplicationType;
        this.characterStat = characterStat;
    }

    public void ApplyModifier(ref StatModifier statModifier, ref CharacterStat characterStat, StatModApplicationType speedModApplicationType)
    {

    }


    public void RemoveModifer(ref StatModifier statModifier, ref CharacterStat characterStat, StatModApplicationType speedModApplicationType)
    {

    }


    public void ApplyModifierForSeconds(ref StatModifier statModifier, ref CharacterStat characterStat, StatModApplicationType speedModApplicationType, float seconds)
    {

    }

    //IEnumerator modDuration(ref StatModifier statModifier, ref CharacterStat characterStat, SpeedModApplicationType speedModApplicationType, float seconds)
    //{


    //    ApplyModifier(ref statModifier,ref characterStat, speedModApplicationType);
    //    yield return new WaitForSeconds(seconds);
    //    RemoveModifer(ref statModifier,ref characterStat, speedModApplicationType);
    //}
}
