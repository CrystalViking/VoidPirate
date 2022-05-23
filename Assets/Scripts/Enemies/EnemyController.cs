using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum EnemyState
{
    Idle,
    Wander,
    Follow,
    Die,
    Attack
};

public enum EnemyType
{
    Melee,
    Ranged
};

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float attackRange;
    public GameObject bullet;
    public float timeBetweenAttacks;
    private float nextAttackTime;
    private Transform player;
    private Animator anim;
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public EnemyState currState = EnemyState.Idle;
    public EnemyType enemyType;
    public bool isInRoom = true;
    float distanceFromPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        switch (currState)
        {

            case (EnemyState.Idle):
                Idle();
                break;
            case (EnemyState.Wander):
                //Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (isInRoom)
        {
            if (IsInLineOfSight() && currState != EnemyState.Die)
            {
                currState = EnemyState.Follow;
            }
            else if (!IsInLineOfSight() && currState != EnemyState.Die)
            {
                currState = EnemyState.Idle; //Should Wander
            }
            if (CanAttack())
            {
                currState = EnemyState.Attack;
            }
        }
        else
        {
            currState = EnemyState.Idle;
        }


    }

    void Attack()
    {
        switch (enemyType)
        {
            case (EnemyType.Melee):
                anim.SetBool("IsAttacking", true);
                break;

            case (EnemyType.Ranged):
                anim.SetBool("IsAttacking", true);
                Instantiate(bullet, transform.position, Quaternion.identity);
                nextAttackTime = Time.time + timeBetweenAttacks;

                break;
        }
    }

    void Follow()
    {
        switch (enemyType)
        {
            case (EnemyType.Melee):
                anim.SetBool("IsMoving", true);
                anim.SetBool("IsAttacking", false);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

                break;
            case (EnemyType.Ranged):
                anim.SetBool("IsAttacking", false);
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                break;
        }
    }

    void Idle()
    {
        switch (enemyType)
        {
            case (EnemyType.Melee):
                anim.SetBool("IsMoving", false);
                break;

            case (EnemyType.Ranged):
                anim.SetBool("IsAttacking", false);
                //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
                break;
        }
    }

    private bool IsInLineOfSight()
    {
        return distanceFromPlayer < lineOfSight;
    }

    private bool IsInAttackRange()
    {
        return distanceFromPlayer < attackRange;
    }

    private bool IsItTimeToAttack()
    {
        return Time.time > nextAttackTime;
    }

    private bool CanAttack()
    {
        return IsInAttackRange() && IsItTimeToAttack();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void DealDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        healthBarSlider.value = CalculateHealthPercentage();
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            healthBar.SetActive(false);
            anim.SetBool("IsDead", true);
            if ((gameObject.GetComponent("FollowEnemy") as FollowEnemy) != null)
            {
                GetComponent<FollowEnemy>().enabled = false;
            }
            else if ((gameObject.GetComponent("ShootingEnemy") as ShootingEnemy) != null)
            {
                GetComponent<ShootingEnemy>().enabled = false;
            }
            currState = EnemyState.Die;
            Destroy(gameObject, 10f);
        }
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

}
