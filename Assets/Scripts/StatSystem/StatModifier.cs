using System.Collections;
using System.Collections.Generic;

public enum StatModType
{
    Flat,
    Percent,
}


public class StatModifier
{
    public readonly float Value;
    public readonly StatModType Type;
    public readonly int Order;
    public StatModifier(float value, StatModType type, int order)
    {
        Value = value;
        Type = type;
        Order = order;
    }

    public StatModifier(float value, StatModType type): this (value, type, (int)type) { }

}
