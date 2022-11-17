using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemyData", menuName = "Enemy/EnemyFightData")]
public class EnemyData : ScriptableObject
{
    public float speed;
    public float lineOfSight;
    public float attackRange;
    public float timeBetweenAttacks;
    public float meleeDamage;
    public float maxHealth;

    public float meleeAnimationDelay;
    public float meleeAnimationDamageDelay;

    public bool useRoomLogic = false;
    public bool activeBehaviour = true;

    public float despawnTimer;

}
