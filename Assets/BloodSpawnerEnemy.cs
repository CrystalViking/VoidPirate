using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodSpawnerEnemy : MeleeEnemy
{
    new private MeleeEnemyAnimator animator;
    private bool attacked;
    private bool moneySpawned;
    private Task jump;
    private bool targetset = false;
    private Vector2 target;
    //public AudioSource bite;
    void Start()
    {
        health = enemyData.maxHealth;
        useRoomLogic = enemyData.useRoomLogic;
        activeBehaviour = enemyData.activeBehaviour;
        isUndestructible = false;

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
                JumpAttack();
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
        if (enemyCalculations.IsItTimeToAttack() && currState != EnemyState.Die)
        {
            targetset = false;
            currState = EnemyState.Attack;
        }
    }

    public new void PassiveBehaviour()
    {
        currState = EnemyState.Idle;
        enemyCalculations.SetNextAttackTime();
    }

    public void JumpAttack()
    {
        animator.SetIsMovingFalse();
        attacked = false;
        animator.SetIsAttackingTrue();
        if (!targetset)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform.position;
            jump = new Task(TeleportCoroutine(target));
            targetset = true;
        }      
        delayDMG = new Task(DelayDMG());
        delay = new Task(Delay());
    }

    IEnumerator TeleportCoroutine(Vector2 _target)
    {
        Vector2 current = transform.position;
        Vector2 target = _target;
        float step = 10f * Time.deltaTime;
        while (Vector2.Distance(current, target) > 0.5)
        {      
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            current = transform.position;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public override void Follow()
    {
        animator.SetIsMovingTrue();
        animator.SetIsAttackingFalse();
        if (!audioSource.isPlaying)
        audioSource.Play();
       
        if(useAStar)
            astar.Move(enemyData.speed * 150);
        else
            transform.position = enemyMovement.MoveEnemy(transform.position, enemyData.speed);

    }

    public override void Idle()
    {
        animator.SetIsMovingFalse();
        animator.SetIsAttackingFalse();
    }

    protected override IEnumerator Delay()
    {
        //animator.SetIsAttackingTrue();
        enemyCalculations.SetNextAttackTime();      
        yield return new WaitForSeconds(enemyData.meleeAnimationDelay);        
        currState = EnemyState.Idle;
        if (health < 0)
            currState = EnemyState.Die;

    }

    protected override IEnumerator DelayDMG()
    {
        yield return new WaitForSeconds(0.5f);
        if (!attacked && enemyCalculations.IsInAttackRange())
        {
            bite.Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyData.meleeDamage);          
        }
        attacked = true;
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
            CheckDeath();
        }
    }
    protected override void CheckDeath()
    {
        base.CheckDeath(healthBar, animator, gameObject);
    }
}
