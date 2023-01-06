using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodySpawnerEnemy : RangedEnemy
{
    private bool moneySpawned;
    public GameObject[] targets;
    public GameObject target;
    public GameObject bomber;
    private GameObject[] bombs;
    private bool spawned;
    public AstarAI astar;


    void Start()
    {
        health = enemyData.maxHealth;
        currState = EnemyState.Idle;
        activeBehaviour = enemyData.activeBehaviour;
        useRoomLogic = enemyData.useRoomLogic;
        isUndestructible = false;
        spawned = false;
        bombs = new GameObject[6];

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
            
        Vector3 vectorToTarget = GameObject.FindGameObjectWithTag("Player").transform.position - target.transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        target.transform.rotation = Quaternion.Slerp(target.transform.rotation, q, Time.deltaTime * 240f);
    }

    public new void ScrollStates()
    {
        switch (currState)
        {

            case (EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Move):
                Move();
                break;
            case (EnemyState.Attack):
                Attack();
                break;
            case (EnemyState.Die):
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
        if (enemyCalculations.IsInLineOfSight() && !enemyCalculations.CanAttack() && currState != EnemyState.Die)
        {
            currState = EnemyState.Move;
        }
        else if (enemyCalculations.IsInLineOfSight() && enemyCalculations.CanAttack() && currState != EnemyState.Die)
        {
            currState = EnemyState.Attack;
        }
        else if (!enemyCalculations.IsInLineOfSight() && currState != EnemyState.Die)
        {
            currState = EnemyState.Idle;
        }

    }

    public new void PassiveBehaviour()
    {
        currState = EnemyState.Idle;
        enemyCalculations.SetNextAttackTime();
    }

    public override void Idle()
    {
        animator?.SetIsAttackingFalse();
        animator?.SetIsMovingFalse();
    }

    public void Move()
    {
        animator.SetIsAttackingFalse();
        animator.SetIsMovingTrue();
        //transform.position = enemyMovement.MoveEnemy(transform.position, enemyData.speed);
        astar.Move(enemyData.speed * 100);
    }

    public new void Attack()
    {
        animator.SetIsAttackingTrue();       
        if (enemyCalculations.CanAttack())
        {
            audioSource.Play();     
            GameObject[] orbs = new GameObject[5];
            for (int i = 0; i < targets.Length; i++)
            {
                orbs[i] = Instantiate(projectile, transform.position, Quaternion.identity);
                orbs[i].GetComponent<EstrellaSaws_v1_1>().SetGameObject(targets[i]);
            }       
        }
        enemyCalculations.SetNextAttackTime();

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
            if(health <= 0 && !spawned)
            {
                spit.Play();
                bombs[0] = Instantiate(bomber, new Vector2(transform.position.x + 0.2f, transform.position.y), Quaternion.identity);
                bombs[1] = Instantiate(bomber, new Vector2(transform.position.x, transform.position.y + 0.2f), Quaternion.identity);
                bombs[2] = Instantiate(bomber, new Vector2(transform.position.x + 0.2f, transform.position.y + 0.2f), Quaternion.identity);
                bombs[3] = Instantiate(bomber, new Vector2(transform.position.x - 0.2f, transform.position.y), Quaternion.identity);
                bombs[4] = Instantiate(bomber, new Vector2(transform.position.x, transform.position.y - 0.2f), Quaternion.identity);
                bombs[5] = Instantiate(bomber, new Vector2(transform.position.x - 0.2f, transform.position.y - 0.2f), Quaternion.identity);

                bombs[0].transform.parent = transform.parent;
                bombs[1].transform.parent = transform.parent;
                bombs[2].transform.parent = transform.parent;
                bombs[3].transform.parent = transform.parent;
                bombs[4].transform.parent = transform.parent;
                bombs[5].transform.parent = transform.parent;
                spawned = true;

            }
            CheckDeath();
        }
    }

    protected override void CheckDeath()
    {
        base.CheckDeath(healthBar, animator, gameObject);
    }
}
