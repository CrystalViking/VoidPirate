using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandelerEnemy : RangedEnemy
{
    private bool moneySpawned;
    public GameObject flameTarget;
    void Start()
    {
        health = enemyData.maxHealth;
        currState = EnemyState.Idle;
        activeBehaviour = enemyData.activeBehaviour;
        useRoomLogic = enemyData.useRoomLogic;
        isUndestructible = false;

        enemyMovement = GetComponent<RangedEnemyMovement>();

        animator = GetComponent<RangedEnemyAnimator>();

        healthBar = GetComponent<HealthBar>();

        enemyCalculations = GetComponent<RangedEnemyCalculations>();

        enemyMovement.SetPlayerTransform(GameObject.FindGameObjectWithTag("Player").transform);
    }


    void Update()
    {
        if (currState != EnemyState.Die)
        {
            ScrollStates();
            SelectBehaviour();
        }

        flameTarget.transform.RotateAround(transform.position, Vector3.forward, 1240 * Time.deltaTime);

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
                //RangedAttack();              
                StartCoroutine(FlameAttack());              
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

    public new void PassiveBehaviour()
    {
        currState = EnemyState.Idle;
        enemyCalculations.SetNextAttackTime();
    }

    public new void ActiveBehaviour()
    {
        if (enemyCalculations.IsInLineOfSight() && !enemyCalculations.CanAttack() && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if (!enemyCalculations.IsInLineOfSight() && currState != EnemyState.Die)
        {
            currState = EnemyState.Idle;
        }
        else if (enemyCalculations.IsInAttackRange() && enemyCalculations.CanAttack() && currState != EnemyState.Die)
        {
            if(!audioSource.isPlaying)
                audioSource.Play();
            currState = EnemyState.Attack;
        }

    }
    IEnumerator FlameAttack()
    {
        animator.SetIsAttackingTrue();      
        GameObject flameprojectile = Instantiate(projectile, new Vector2(transform.position.x - 0.5f, transform.position.y + 0.5f), Quaternion.identity);
        flameprojectile.GetComponent<EstrellaSaws_v1_1>().SetGameObject(flameTarget);
        yield return new WaitForSeconds(1f);
        StartCoroutine(StopAudioSource());
        animator.SetIsAttackingFalse();
        enemyCalculations.SetNextAttackTime();       
        currState = EnemyState.Follow;
        if (health < 0)
            currState = EnemyState.Die;
    }

    IEnumerator StopAudioSource()
    {
        yield return new WaitForSeconds(1f);
        audioSource.Stop();
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
        if (!isUndestructible)
        {
            if (!particles.isPlaying)
                particles.Play();
            healthBar.SetHealthBarActive();
            health -= damage;
            if (health > enemyData.maxHealth)
                health = enemyData.maxHealth;
            healthBar.SetHealthBarValue(enemyCalculations.CalculateHealthPercentage(health));
            CheckDeath(healthBar, animator, gameObject);
        }
    }

    protected override void CheckDeath()
    {
        base.CheckDeath(healthBar, animator, gameObject);
    }
}
