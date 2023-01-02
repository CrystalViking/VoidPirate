using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectorEnemy : MeleeEnemy
{
  new private ProtectorEnemyAnimator animator;
  public GameObject ally;
  private bool autioset;
  private bool moneySpawned;

  void Start()
  {
    health = enemyData.maxHealth;
    useRoomLogic = enemyData.useRoomLogic;
    activeBehaviour = enemyData.activeBehaviour;
    isUndestructible = false;

    animator = GetComponent<ProtectorEnemyAnimator>();

    healthBar = GetComponent<HealthBar>();

    enemyCalculations = GetComponent<MeleeEnemyCalculations>();
    autioset = false;

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
      case (EnemyState.Protect):
        if (!autioset)
        {
          audioSource.Play();
          autioset = true;
        }
        Protect();
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
    if (!ally && currState != EnemyState.Die)
      ally = FindClosestEnemy();
    if (ally && currState != EnemyState.Die)
    {
      currState = EnemyState.Protect;
    }
    else if (currState != EnemyState.Die)
    {
      currState = EnemyState.Follow;
    }
    else
    {
      currState = EnemyState.Idle;
    }
  }

  public new void PassiveBehaviour()
  {
    currState = EnemyState.Idle;
  }

  public override void Idle()
  {
    animator.SetIsProtectingFalse();
    try
    {
      if (ally)
        ReturnHP();
    }
    catch { }
  }

  public new void Follow()
  {
    transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, enemyData.speed * 1 / 4 * Time.deltaTime);
  }

  public void Protect()
  {
    if (!ally)
    {
      ally = FindClosestEnemy();
      animator.SetIsProtectingFalse();
    }
    else
    {
      animator.SetIsProtectingTrue();
      ally.GetComponent<IEnemy>().SetUndestructible(true);
      transform.position = Vector2.MoveTowards(transform.position, new Vector3(ally.transform.position.x, ally.transform.position.y + 0.5f, ally.transform.position.z), enemyData.speed * Time.deltaTime);
    }
  }

  public void ReturnHP()
  {
    ally.GetComponent<IEnemy>().SetUndestructible(false);
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
      if (!go.GetComponent<ProtectorEnemy>() && GetParent().transform.parent.gameObject == go.GetComponent<IEnemy>().GetParent().transform.parent.gameObject)
      {
        Vector3 diff = go.transform.position - position;
        float curDistance = diff.sqrMagnitude;
        if (curDistance < distance && curDistance > 0.0f)
        {
          closest = go;
          distance = curDistance;
        }
      }

    }
    return closest;
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

      if (ally)
        ReturnHP();

      audioSource.Stop();
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