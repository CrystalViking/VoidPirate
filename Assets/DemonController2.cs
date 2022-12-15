using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DemonController2 : Enemy, IEnemy
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
    public GameObject ally;
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
            case (EnemyState.Heal):
                StartCoroutine(Heal());
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
                currState = EnemyState.Heal;
            }
            else
            {
                currState = EnemyState.Idle;
            }


            nextAttackTime = Time.time + timeBetweenAttacks;
        }

    }

    IEnumerator Heal()
    {
        anim.SetBool("IsHealing", true);

        yield return new WaitForSeconds(0.555f);

        if (!attackedPlayer)
        {
            audioSource.Play();
            StartCoroutine(StopAudio());
            ally = FindClosestEnemy();
            if(ally)
            {
                ally.GetComponent<DemonController>().TakeDamage(-200, true);
                attackedPlayer = true;
            }
            
        }

        anim.SetBool("IsHealing", false);
        currState = EnemyState.Idle;
        if (health < 0)
            currState = EnemyState.Die;
    }

    IEnumerator StopAudio()
    {
        yield return new WaitForSeconds(1.4f);
        audioSource.Stop();
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
            if (go.GetComponent<DemonController>())
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
}
