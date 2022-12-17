using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonController : Enemy, IEnemy
{
    public float speed;
    public float attackRange;
    public GameObject bullet;
    public float timeBetweenAttacks;
    private Transform player;
    private Animator anim;
    public float maxHealth;
    public bool isDead = false;
    float distanceFromPlayer;
    //public AudioSource[] audioSources;
    public ParticleSystem particles;
    private bool attackedPlayer = false;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        nextAttackTime = 1.0f;
        //useRoomLogic = true;
        healthBar = GetComponent<HealthBar>();
    }

    void Update()
    {
        switch (currState)
        {

            case (EnemyState.Idle):
                break;                             
            case (EnemyState.RangeAttack):
                StartCoroutine(OrbAttack());
                break;
            case (EnemyState.Die):
                break;
        }

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (activeBehaviour)
        {
            if (health > 0)
                PrepareNextMove();
        }
        else
        {
            currState = EnemyState.Idle;
        }

    }

    public new void TakeDamage(float damage)
    {
        if (!particles.isPlaying)
            particles.Play();
        healthBar.SetHealthBarActive();
        health -= damage;
        if (health > maxHealth)
            health = maxHealth;
        healthBar.SetHealthBarValue(CalculateHealthPercentage());
        CheckDeath();
    }

    public void TakeDamage(float damage, bool t)
    {
        healthBar.SetHealthBarActive();
        health -= damage;
        if (health > maxHealth)
            health = maxHealth;
        healthBar.SetHealthBarValue(CalculateHealthPercentage());
        CheckDeath();
    }

    private new void CheckDeath()
    {
        if (health <= 0)
        {
            currState = EnemyState.Die;
            healthBar.SetHealthBarInActive();
            anim.SetBool("IsDead", true);

            Destroy(gameObject, 1.0f);
        }
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    private bool IsInAttackRange()
    {
        return distanceFromPlayer < attackRange;
    }

    private void PrepareNextMove()
    {
        if (Time.time > nextAttackTime)
        {
            int r = 0;
            r = Random.Range(1, 3);

            if (r == 1)
            {
                attackedPlayer = false;
                currState = EnemyState.RangeAttack;
            }
            else
            {
                currState = EnemyState.Idle;
            }


            nextAttackTime = Time.time + timeBetweenAttacks;
        }
        
    }

    IEnumerator OrbAttack()
    {
        anim.SetBool("IsOrbing", true);

        yield return new WaitForSeconds(0.555f);

        if (!attackedPlayer)
        {
            audioSource.Play();
            Instantiate(bullet, transform.position, Quaternion.identity);
            attackedPlayer = true;
        }

        anim.SetBool("IsOrbing", false);
        currState = EnemyState.Idle;
        if (health < 0)
            currState = EnemyState.Die;
    }
}
