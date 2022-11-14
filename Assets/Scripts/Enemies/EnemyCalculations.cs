using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCalculations : MonoBehaviour, IEnemyCalculations
{
    protected float nextAttackTime;
    [SerializeField]
    protected EnemyData enemyData;

    //public EnemyCalculations() { }

    //public EnemyCalculations(EnemyData enemyData)
    //{
    //    this.enemyData = enemyData ?? throw new ArgumentNullException(nameof(enemyData));
    //}

    //public EnemyCalculations(EnemyData enemyData, float health)
    //{
    //    this.enemyData = enemyData ?? throw new ArgumentNullException(nameof(enemyData));
    //    SetHealth(health);
    //}

    
    public void SetEnemyData(EnemyData enemyData)
    {
        this.enemyData = enemyData;
    }

    public virtual float CalculateDistanceFromPlayer()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        return distanceFromPlayer;
    }

    public virtual void SetNextAttackTime()
    {
        nextAttackTime = Time.time + enemyData.timeBetweenAttacks;
    }

    public virtual bool IsInLineOfSight()
    {
        return CalculateDistanceFromPlayer() < enemyData.lineOfSight;
    }

    public virtual bool IsInAttackRange()
    {
        return CalculateDistanceFromPlayer() <= enemyData.attackRange;
    }

    public virtual bool IsItTimeToAttack()
    {
        return Time.time > nextAttackTime;
    }

    public bool CanAttack()
    {
        return IsInAttackRange() && IsItTimeToAttack();
    }

    

    public float CalculateHealthPercentage(float healthValue)
    {
        return (healthValue / enemyData.maxHealth);
    }


}
