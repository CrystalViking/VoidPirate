using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MeleeEnemy
{
    new private SpawnerEnemyAnimator animator;
    private bool spawned;
    private Task delaySpawn;
    public GameObject[] allies;
    private int X;
    public GameObject spawnedAlly;

    void Start()
    {
        health = enemyData.maxHealth;
        useRoomLogic = enemyData.useRoomLogic;
        activeBehaviour = enemyData.activeBehaviour;

        animator = GetComponent<SpawnerEnemyAnimator>();

        healthBar = GetComponent<HealthBar>();

        enemyCalculations = GetComponent<MeleeEnemyCalculations>();

        spawned = false;
        
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
            case (EnemyState.Die):
                break;           
            case (EnemyState.Spawn):
                Spawn();
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
        if(enemyCalculations.IsItTimeToAttack() && currState != EnemyState.Die)
        {
            currState = EnemyState.Spawn;
        }
    }

    public new void PassiveBehaviour()
    {
        currState = EnemyState.Idle;
    }

    public override void Idle()
    {
        animator.SetIsSpawningFalse();
    }

    public void Spawn()
    {
        animator.SetIsSpawningTrue();
        spawned = false;
        delaySpawn = new Task(DelaySpawn());
        delay = new Task(Delay());

    }

    protected override IEnumerator Delay()
    {
        enemyCalculations.SetNextAttackTime();
        yield return new WaitForSeconds(enemyData.meleeAnimationDelay);
        if(spawnedAlly)
            spawnedAlly.GetComponent<IEnemy>().SetActiveBehaviourTrue();
        currState = EnemyState.Idle;
        if (health < 0)
            currState = EnemyState.Die;

    }

    protected  IEnumerator DelaySpawn()
    {     
        yield return new WaitForSeconds(enemyData.meleeAnimationDamageDelay);
        if (!spawned && currState != EnemyState.Die)
        {
            X = Random.Range(0, 5);
            spawnedAlly = Instantiate(allies[X], new Vector2(transform.position.x - 0.2f, transform.position.y), Quaternion.identity);
            spawned = true;

        }       

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
