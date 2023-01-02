using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechaHealerEnemy : MeleeEnemy
{
  new private MechaHealerEnemyAnimator animator;
  private bool attacked;
  private bool healed;
  public GameObject enemy;
  private Task delayHeal;
  private bool moneySpawned;
  void Start()
  {
    health = enemyData.maxHealth;
    currState = EnemyState.Idle;
    activeBehaviour = enemyData.activeBehaviour;
    useRoomLogic = enemyData.useRoomLogic;
    isUndestructible = false;
    enemyMovement = GetComponent<MeleeEnemyMovement>();

    animator = GetComponent<MechaHealerEnemyAnimator>();

    healthBar = GetComponent<HealthBar>();

    attacked = false;
    healed = false;

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
      case (EnemyState.Heal):
        Heal();
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
    enemy = FindClosestEnemy();
    if (!enemyCalculations.CanAttack() && !enemyCalculations.IsInAttackRange() && !enemy && currState != EnemyState.Die)
    {
      currState = EnemyState.Follow;
    }
    if (enemyCalculations.CanAttack() && currState != EnemyState.Die)
    {
      currState = EnemyState.Attack;
    }
    else if (!enemyCalculations.CanAttack() && enemy && enemyCalculations.IsItTimeToAttack() && currState != EnemyState.Die)
    {
      currState = EnemyState.Heal;
    }
  }

  public new void PassiveBehaviour()
  {
    currState = EnemyState.Idle;
  }

  public GameObject FindClosestEnemy()
  {
    GameObject[] gos;
    gos = GameObject.FindGameObjectsWithTag("Enemy");
    GameObject closest = null;
    float distance = Mathf.Infinity;
    Vector3 position = transform.position;
    foreach (GameObject go in gos)
    {
      if (!go.GetComponent<IEnemy>().HasFullHealth())
      {
        Vector3 diff = go.transform.position - position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance < distance && curDistance > 0.0f && curDistance < 30.0f)
        {
          closest = go;
          distance = curDistance;
        }
      }
    }
    return closest;
  }

  public void Heal()
  {
    animator.SetIsMovingFalse();
    animator.SetIsHealingTrue();
    healed = false;
    audioSource.Play();
    delayHeal = new Task(DelayHeal());
    delay = new Task(Delay());

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
    animator.SetIsHealingFalse();

    transform.position = enemyMovement.MoveEnemy(transform.position, enemyData.speed);
  }

  public override void Idle()
  {
    animator.SetIsMovingFalse();
    animator.SetIsAttackingFalse();
    animator.SetIsHealingFalse();
  }

  protected override IEnumerator Delay()
  {
    if (currState == EnemyState.Attack)
      animator.SetIsAttackingTrue();
    enemyCalculations.SetNextAttackTime();
    if (currState == EnemyState.Attack)
      yield return new WaitForSeconds(enemyData.meleeAnimationDelay);
    else
      yield return new WaitForSeconds(enemyData.meleeAnimationDelay * 4);
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
      attacked = true;
    }
  }

  protected IEnumerator DelayHeal()
  {
    yield return new WaitForSeconds(enemyData.meleeAnimationDamageDelay * 3);
    if (!healed && enemy)
    {
      if (enemy.GetComponent<IEnemy>().GetEnemyState() != EnemyState.Die)
        enemy.GetComponent<IEnemy>().TakeDamage(-200);
      healed = true;
    }
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
    if (health <= 0)
    {
      DestroyedEvent destroyedEvent = GetComponent<DestroyedEvent>();
      destroyedEvent.CallDestroyedEvent(false, 1);
      healthBar.SetHealthBarInActive();
      animator.SetIsDeadTrue();

      currState = EnemyState.Die;

      if (useRoomLogic)
        RoomController.instance.StartCoroutine(RoomController.instance.RoomCorutine());

      if (!moneySpawned)
      {
        Instantiate(cashParticles, transform.position, Quaternion.identity);
        moneySpawned = true;
      }

      Destroy(gameObject, enemyData.despawnTimer);

    }
  }
}
