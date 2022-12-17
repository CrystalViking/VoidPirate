using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new PlayerStats", menuName = "Stats/PlayerStats")]
public class PlayerStatsSO : ScriptableObject
{
    public float maxHealth;
    public float speed;
    public int weaponInventorySize;
}
