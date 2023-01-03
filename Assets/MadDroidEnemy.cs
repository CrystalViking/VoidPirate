using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadDroidEnemy : RangedEnemy
{
  new private MadEnemyAnimator animator;
  private bool moneySpawned;

  void Start()
  {
    health = enemyData.maxHealth;
    currState = EnemyState.Idle;
    activeBehaviour = enemyData.activeBehaviour;
    useRoomLogic = enemyData.useRoomLogic;
    isUndestructible = false;

    enemyMovement = GetComponent<MeleeEnemyMovement>();

    animator = GetComponent<MadEnemyAnimator>();

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

  }

  public new void ScrollStates()
  {
    switch (currState)
    {

      case (EnemyState.Idle):
        Idle();
        break;
      case (EnemyState.FollowAndAttack):
        FollowAndAttack();
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
    if (enemyCalculations.IsInLineOfSight() && currState != EnemyState.Die)
    {
      currState = EnemyState.FollowAndAttack;
    }
    else if (!enemyCalculations.IsInLineOfSight() && currState != EnemyState.Die)
    {
      currState = EnemyState.Idle;
    }

  }

    public new void PassiveBehaviour()
    {
        currState = EnemyState.Idle;
    }

    public void FollowAndAttack()
  {
    animator.SetIsMovingOrAttackingTrue();
    transform.position = enemyMovement.MoveEnemy(transform.position, enemyData.speed);
    if (enemyCalculations.CanAttack())
    {
      audioSource.Play();
      Instantiate(projectile, new Vector3(transform.position.x, transform.position.y + 0.6f, transform.position.z), Quaternion.identity);
      enemyCalculations.SetNextAttackTime();
    }

  }

  public override void Idle()
  {
    animator?.SetIsMovingOrAttackingFalse();
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
