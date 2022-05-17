using System;
using System.Collections;
using System.Collections.Generic;

public class CharacterStat 
{
    public float BaseValue { get; set; }

    public float Value
    {
        get
        {
            if (isDirty)
            {
                _value = CalculateFinalValue();
                isDirty = false;
            }
            return _value;
        }
    }

    private bool isDirty = true;
    private float _value;

    private readonly List<StatModifier> statModifiers;

    public CharacterStat(float baseValue)
    {
        BaseValue = baseValue;
        statModifiers = new List<StatModifier>();
    }

    public void AddModifier(StatModifier mod)
    {
        isDirty = true;
        statModifiers.Add(mod);
        statModifiers.Sort(CompareModifierOrder);
    }

    private int CompareModifierOrder(StatModifier a, StatModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }

    public bool RemoveModifier(StatModifier mod)
    {
        isDirty = true;
        return statModifiers.Remove(mod);
    }

    private float CalculateFinalValue()
    {
        float finalValue = BaseValue;

        foreach(StatModifier mod in statModifiers)
        {
            if(mod.Type == StatModType.Flat)
            {
                finalValue += mod.Value;
            }
            else if(mod.Type == StatModType.Percent)
            {
                finalValue *= 1 + mod.Value;
            }
        }


        return (float)Math.Round(finalValue, 4);


    }
}
