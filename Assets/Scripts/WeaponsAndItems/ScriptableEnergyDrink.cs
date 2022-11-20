using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new EnergyDrink", menuName = "Items/EnergyDrink")]
public class ScriptableEnergyDrink : Item
{
    public float duration;
    public float hp_content;
    public float percentBuff;
}
