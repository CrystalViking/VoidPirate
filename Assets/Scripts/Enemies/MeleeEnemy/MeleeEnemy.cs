using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{

    Task delayDMG;
    Task delay;
    Task roomCoroutine;


    bool isDead = false;

    void Start()
    {
        health = enemyData.maxHealth;
        useRoomLogic = enemyData.useRoomLogic; 
        activeBehaviour = enemyData.activeBehaviour;

        enemyMovement = GetComponent<MeleeEnemyMovement>();
        
        animator = GetComponent<MeleeEnemyAnimator>();

        healthBar = GetComponent<HealthBar>();

        enemyCalculations = GetComponent<MeleeEnemyCalculations>();
        enemyMovement.SetPlayerTransform(GameObject.FindGameObjectWithTag("Player").transform);
    }

    
    void Update()
    {
        if (currState != EnemyState.Die)
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
                //delayDMG.Stop();
                //delay.Stop();
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

    public void ActiveBehaviour()
    {
        if(enemyCalculations.IsInLineOfSight() && currState != 
            EnemyState.Die && !enemyCalculations.IsInAttackRange())
        {
            currState = EnemyState.Follow;
        }
        else if (!enemyCalculations.IsInLineOfSight() && currState != EnemyState.Die)
        {
            currState = EnemyState.Idle;
        }
        if (enemyCalculations.CanAttack() && currState != EnemyState.Die)
        {
            currState = EnemyState.Attack;
            delayDMG = new Task(DelayDMG());
            //StartCoroutine(DelayDMG());
        }
    }

    public void PassiveBehaviour()
    {
        currState = EnemyState.Idle;
    }

    public override void Attack()
    {
        animator.SetIsMovingFalse();
        //StartCoroutine(Delay());
        delay = new Task(Delay());
    }

    public override void Follow()
    {
        animator.SetIsMovingTrue();
        animator.SetIsAttackingFalse();

        
        transform.position = enemyMovement.MoveEnemy(transform.position, enemyData.speed);
    }

    public override void Idle()
    {
        animator.SetIsMovingFalse();
        animator.SetIsAttackingFalse();
    }

    protected override IEnumerator Delay()
    {
        animator.SetIsAttackingTrue();
        enemyCalculations.SetNextAttackTime();
        yield return new WaitForSeconds(enemyData.meleeAnimationDelay);
        currState = EnemyState.Idle;
        if (isDead)
            currState = EnemyState.Die;

    }

    protected override IEnumerator DelayDMG()
    {
        yield return new WaitForSeconds(enemyData.meleeAnimationDamageDelay);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyData.meleeDamage);
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
            isDead = true;

            if (useRoomLogic)
                RoomController.instance.StartCoroutine(RoomController.instance.RoomCorutine());

            Destroy(gameObject, enemyData.despawnTimer);

        }
    }

    

    



}
