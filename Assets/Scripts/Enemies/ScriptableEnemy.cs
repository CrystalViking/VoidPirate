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
    private float nextAttackTime;
    private Transform player;
    private Animator anim;
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;
    public bool isInRoom = true;
    float distanceFromPlayer;
}
