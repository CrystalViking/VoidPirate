using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

[Serializable]
public class CharacterStat 
{
    public float BaseValue;


    public virtual float Value
    {
        get
        {
            if (isDirty || BaseValue != lastBaseValue)
            {
                lastBaseValue = BaseValue;
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    protected bool isDirty = true;
    protected float _value;
    protected float lastBaseValue = float.MinValue;

    protected readonly List<StatModifier> statModifiers;
    public readonly ReadOnlyCollection<StatModifier> StatModifiers;


    public CharacterStat()
    {
        statModifiers = new List<StatModifier>();
        StatModifiers = statModifiers.AsReadOnly();
    }

    public CharacterStat(float baseValue): this()
    {
        BaseValue = baseValue;
    }

    public virtual void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    public virtual bool RemoveAllModifiersFromSource(object source)
    {
        bool didRemove = false;

        for (int i = statModifiers.Count - 1; i >= 0; i--)
        {
            if (statModifiers[i].Source == source)
            {
                isDirty = true;
                didRemove = true;
                statModifiers.RemoveAt(i);
            }
        }

        return didRemove;
    }

    protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }

    public virtual bool RemoveModifier(StatModifier mod)
    {
        if (statModifiers.Remove(mod))
        {
            isDirty = true;
            return true;
        }
        return false;
    }

    protected virtual float CalculateFinalValue()
    {
        float finalValue = BaseValue;
        float sumePercentAdd = 0;

        foreach(StatModifier mod in statModifiers)
        {
            if(mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if (mod.Type == StatModType.PercentAdd)
            {
                sumePercentAdd += mod.Value;

                if (statModifiers.IndexOf(mod) + 1 >= statModifiers.Count 
                    || 
                    statModifiers[statModifiers.IndexOf(mod) + 1].Type != StatModType.PercentAdd)
                {
                    finalValue *= 1 + sumePercentAdd;
                    sumePercentAdd = 0;
                }
            }
            else if(mod.Type == StatModType.PercentMult)
            {
                finalValue *= 1 + mod.Value;
            }
        }


        return (float)Math.Round(finalValue, 4);


    }
}
