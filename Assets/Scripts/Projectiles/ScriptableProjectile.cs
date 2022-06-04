using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[CreateAssetMenu(fileName = "new Projectile", menuName = "Items/Projectile")]
public class ScriptableProjectile : Item
{
    public float speed;
    public float damage;
    public float projectileDestroyTime;
}

