using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Die,
    Attack,
    FollowAndAttack,
    Heal,
    Spawn,
    Protect,
    Move,
    SawAttack,
    FlameAttack,
    Debuff,
    MeleeAttack,
    RangeAttack,
    Consume,
    Teleport,
    Reborn,
    MeleeAttackReborned,
    MoveReborned
}

public abstract class Enemy : MonoBehaviour, IEnemy
{
    public AudioSource audioSource;
    [SerializeField]
    protected EnemyData enemyData;
    protected float nextAttackTime;
    protected IEnemyCalculations enemyCalculations;
    protected IEnemyAnimator animator;
    protected IEnemyMovement enemyMovement;
    protected HealthBar healthBar;
    public bool useRoomLogic = false;
    public bool activeBehaviour = false;
    public EnemyState currState = EnemyState.Idle;
    public bool isUndestructible = false;
    

    public float health;



    private void Start()
    {
        enemyData = GetComponent<EnemyData>();
        //enemyCalculations.SetEnemyData(enemyData);
    }

    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();

    }

    public virtual void Idle()
    {
        Debug.Log("Enemy: IDLE");
    }

    public virtual void Wander()
    {
        Debug.Log("Enemy: WANDER");
    }

    public virtual void Follow()
    {
        Debug.Log("Enemy: FOLLOW");
    }

    public virtual void Attack()
    {
        Debug.Log("Enemy: ATTACK");
    }

    public virtual void Die()
    {
        Debug.Log("Enemy: DIE");
    }

    public virtual void TakeDamage(float damage)
    {
        Debug.Log("Enemy: Take Damage");
    }

    protected virtual void CheckDeath(HealthBar healthBar, IEnemyAnimator animator, GameObject gameObject)
    {
        Debug.Log("Enemy: Checking Death");
    }

    protected virtual void CheckDeath()
    {
        Debug.Log("Enemy: Checking Death");
    }

    protected virtual void MoveEnemy(Vector2 movementVector)
    {
        Debug.Log("Enemy: Moving");
    }

    public virtual bool HasFullHealth()
    {
        if (enemyData.maxHealth == health)
            return true;
        else
            return false;
    }

    public virtual void SetHealth(float maxhealth)
    {
        health = maxhealth;
    }

    public virtual float GetMaxHealth()
    {
        return enemyData.maxHealth;
    }

    public virtual void SetUndestructible(bool G)
    {
        isUndestructible = G;
    }


    protected virtual IEnumerator Delay()
    {
        throw new NotImplementedException();
    }

    protected virtual IEnumerator DelayDMG()
    {
        throw new NotImplementedException();
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, enemyData.lineOfSight);
        Gizmos.DrawWireSphere(transform.position, enemyData.attackRange);
    }

    public void SetUseRoomLogicTrue()
    {
        useRoomLogic = true;
    }

    public void SetUseRoomLogicFalse()
    {
        useRoomLogic = false;
    }

    public void SetActiveBehaviourFalse()
    {
        activeBehaviour = false;
    }

    public void SetActiveBehaviourTrue()
    {
        activeBehaviour = true;
    }

    public EnemyState GetEnemyState()
    {
        return currState;
    }

    public GameObject GetParent()
    {
        return transform.parent.gameObject;
    }
}
