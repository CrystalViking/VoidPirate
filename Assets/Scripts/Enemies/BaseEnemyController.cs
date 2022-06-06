using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public enum EnemyState1
{
    Idle,
    Wander,
    Follow,
    RangedAttack,
    MeleeAttack
}

public enum EnemyType1
{
    Melee,
    Ranged,
    Hybrid
}



public class BaseEnemyController : MonoBehaviour
{
    [SerializeField] ScriptableEnemy scriptableEnemy = null;

    private float nextAttackTime;
    private Transform player;
    private Animator anim;

    public EnemyState1 currState = EnemyState1.Idle;
    public EnemyType1 enemyType;

    // scriptable stats
    private float speed;
    private float lineOfSight;
    private float attackRange;
    private GameObject[] bullet;
    private float timeBetweenAttacks;
    private float health;
    private float maxHealth;
    private GameObject healthBar;
    private Slider healthBarSlider;

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    


    public void InitVariables()
    {
        speed = scriptableEnemy.speed;
        lineOfSight = scriptableEnemy.lineOfSight;
        attackRange = scriptableEnemy.attackRange;
        bullet = scriptableEnemy.bullet;

    }
}
