using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodyBombEnemy : MeleeEnemy
{
    private float random_booster;
    public ParticleSystem boom;
    void Start()
    {
        health = enemyData.maxHealth;
        useRoomLogic = enemyData.useRoomLogic;
        activeBehaviour = enemyData.activeBehaviour;
        isUndestructible = false;
        random_booster = Random.Range(0.75f, 1.5f);

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

    public new void ScrollStates()
    {
        switch (currState)
        {
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                break;
        }
    }

    public new void SelectBehaviour()
    {
        if (currState != EnemyState.Die)
            currState = EnemyState.Follow;
    }

    public override void Follow()
    {
        animator.SetIsMovingTrue();
        if (!audioSource.isPlaying)
            audioSource.Play();

        transform.position = enemyMovement.MoveEnemy(transform.position, enemyData.speed * random_booster);
        if(Vector2.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position) < 0.5f)
        {
            Explode();
        }

    }

    public void Explode()
    {
        bite.Play();
        boom.Play();
        health = -1000;
        CheckDeath();
        currState = EnemyState.Die;       
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(enemyData.meleeDamage);
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
