using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



[CreateAssetMenu(fileName = "new Enemy", menuName = "Actors/Enemy")]
public class ScriptableEnemy : ScriptableObject
{
    public string EnemyName;
    public string EnemyDescription;
    public Sprite Icon;

    public float speed;
    public float lineOfSight;
    public float attackRange;
    public GameObject[] bullet;
    public float timeBetweenAttacks;
    //private float nextAttackTime; <- move to base class
    //private Transform player; <- move to base class
    //private Animator anim; <- move to base class
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    //public EnemyState currState = EnemyState.Idle; <- move to base class
    //public EnemyType enemyType; <- move to base class
    public bool isInRoom = true;
    
    //float distanceFromPlayer; <- move to base class
}
