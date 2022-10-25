using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum MedkitType
{
    SmallMedkit = 100,
    MediumMedkit = 200,
    LargeMedkit = 300
}


[CreateAssetMenu(fileName = "new Medkit", menuName = "Items/Medkit")]
public class ScriptableMedkit : Item
{
    public MedkitType medkitType;
    public float hp_content;
}
