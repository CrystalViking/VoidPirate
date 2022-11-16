using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsectEnemy : MeleeEnemy
{
    new private MeleeEnemyAnimator animator;
    private bool attacked;
    void Start()
    {
        health = enemyData.maxHealth;
        useRoomLogic = enemyData.useRoomLogic;
        activeBehaviour = enemyData.activeBehaviour;

        enemyMovement = GetComponent<MeleeEnemyMovement>();

        animator = GetComponent<MeleeEnemyAnimator>();

        healthBar = GetComponent<HealthBar>();
        attacked = false;

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

    public new void ScrollStates()
    {
        switch (currState)
        {

            case (EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                break;
            case (EnemyState.Attack):
                MeleeAttack();
                break;
        }
    }

    public new void SelectBehaviour()
    {
        if (activeBehaviour)
            ActiveBehaviour();
        else
            PassiveBehaviour();


    }

    public new void ActiveBehaviour()
    {
        if (enemyCalculations.IsInLineOfSight() && currState !=
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
        }
    }

    public new void PassiveBehaviour()
    {
        currState = EnemyState.Idle;
    }

    public new void MeleeAttack()
    {
        animator.SetIsMovingFalse();
        attacked = false;
        delayDMG = new Task(DelayDMG());
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
        if (health < 0)
            currState = EnemyState.Die;

    }

    protected override IEnumerator DelayDMG()
    {
        yield return new WaitForSeconds(enemyData.meleeAnimationDamageDelay);
        if (!attacked && enemyCalculations.IsInAttackRange())
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyData.meleeDamage);          
        }
        attacked = true;
    }

    public override void TakeDamage(float damage)
    {
        healthBar.SetHealthBarActive();
        health -= damage;
        if (health > enemyData.maxHealth)
            health = enemyData.maxHealth;
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