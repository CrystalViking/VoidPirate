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
    private List<GameObject> minion_list;

    private bool moneySpawned;

    void Start()
    {
        health = enemyData.maxHealth;
        useRoomLogic = enemyData.useRoomLogic;
        activeBehaviour = enemyData.activeBehaviour;
        isUndestructible = false;
        minion_list = new List<GameObject>();

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

        if (minion_list.Count > 0)
        {
            foreach (GameObject m in minion_list)
            {
                if (!m)
                {
                    minion_list.Remove(m);
                    break;
                }
            }
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
            audioSource.Play();
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
        if(spawnedAlly && activeBehaviour)
            spawnedAlly.GetComponent<IEnemy>().SetActiveBehaviourTrue();
        currState = EnemyState.Idle;
        if (health < 0)
            currState = EnemyState.Die;

    }

    protected  IEnumerator DelaySpawn()
    {       
        yield return new WaitForSeconds(enemyData.meleeAnimationDamageDelay);
        if (!spawned && currState != EnemyState.Die && activeBehaviour)
        {
            X = Random.Range(0, allies.Length);
            spawnedAlly = Instantiate(allies[X], new Vector2(transform.position.x - 0.2f, transform.position.y), Quaternion.identity);
            try
            {
                spawnedAlly.transform.parent = transform.parent;
                minion_list.Add(spawnedAlly);
            }
            catch { }
            spawned = true;

        }
        audioSource.Stop();

    }

    public List<GameObject> GetMinionList()
    {
        return minion_list;
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