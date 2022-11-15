using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{
     public GameObject projectile;


    void Start()
    {
        health = enemyData.maxHealth;
        currState = EnemyState.Idle;
        activeBehaviour = enemyData.activeBehaviour;
        useRoomLogic = enemyData.useRoomLogic;
        enemyMovement = GetComponent<RangedEnemyMovement>();
        
        animator = GetComponent<RangedEnemyAnimator>();

        healthBar = GetComponent<HealthBar>();

       

        enemyCalculations = GetComponent<RangedEnemyCalculations>();
        enemyMovement.SetPlayerTransform(GameObject.FindGameObjectWithTag("Player").transform);
    }

    
    void Update()
    {
        if(currState != EnemyState.Die)
        {
            ScrollStates();
            SelectBehaviour();
        }
        
    }

    public void ScrollStates()
    {
        switch (currState)
        {

            case (EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Wander):
                //Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }
    }
    public void SelectBehaviour()
    {
        if (activeBehaviour)
            ActiveBehaviour();
        else
            PassiveBehaviour();


    }

    public void PassiveBehaviour()
    {
        currState = EnemyState.Idle;
    }

    public  void ActiveBehaviour()
    {
        if(enemyCalculations.IsInLineOfSight() && !enemyCalculations.IsInAttackRange() && currState != EnemyState.Die)
        {
            //Follow();
            currState = EnemyState.Follow;
        }
        else if (!enemyCalculations.IsInLineOfSight() && currState != EnemyState.Die)
        {
            currState = EnemyState.Idle;
        }
        if(enemyCalculations.IsInAttackRange() && enemyCalculations.CanAttack() && currState != EnemyState.Die)
        {
            //Attack();
            currState = EnemyState.Attack;
        }
        
    }


    public override void Attack()
    {
        animator.SetIsAttackingTrue();
        if (enemyCalculations.CanAttack())
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            enemyCalculations.SetNextAttackTime();
        }
            
    }

    public override void Follow()
    {
        animator.SetIsAttackingFalse();
        transform.position = enemyMovement.MoveEnemy(transform.position, enemyData.speed);
    }

    public override void Idle()
    {
        animator?.SetIsAttackingFalse();
    }

    public override void TakeDamage(float damage)
    {
        healthBar.SetHealthBarActive();
        health -= damage;
        healthBar.SetHealthBarValue(enemyCalculations.CalculateHealthPercentage(health));
        CheckDeath();
    }

    protected override void CheckDeath()
    {
        if (health <= 0)
        {
            healthBar.SetHealthBarInActive();
            animator.SetIsDeadTrue();

            currState = EnemyState.Die;

            if (useRoomLogic) 
                RoomController.instance.StartCoroutine(RoomController.instance.RoomCorutine());

            Destroy(gameObject, enemyData.despawnTimer);
        }
    }

}
