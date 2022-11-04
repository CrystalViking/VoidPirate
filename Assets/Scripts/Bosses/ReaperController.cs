using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReaperController : MonoBehaviour
{
    public enum BossState
    {
        Idle,
        Move,
        MeleeAttack,
        RangeAttack,
        Spawn,
        Consume,
        Teleport,
        Death

    };

    public float speed;
    public float attackRange;
    public GameObject[] bullet;
    public float timeBetweenAttacks;
    private float nextAttackTime;
    private Transform player;
    private Animator anim;
    public float health;
    public float maxHealth;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public BossState currState = BossState.Idle;
    public bool isInRoom = false;
    public bool isDead = false;
    private bool damagedPlayer = false;
    private bool attackedPlayer = false;
    float distanceFromPlayer;
    //public GameObject portal;
    //private bool isSpawned = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        nextAttackTime = 1.0f;
    }

    void Update()
    {
        switch (currState)
        {

            case (BossState.Idle):
                break;
            case (BossState.Move):
                Move(speed);
                break;
            case (BossState.MeleeAttack):
                Move(speed/2f);
                StartCoroutine(MeleeAttack());
                break;
            case (BossState.RangeAttack):
                StartCoroutine(OrbAttack());              
                break;
            case (BossState.Death):
                //StartCoroutine(SpawnPortal());
            break;
        }

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (isInRoom)
        {
            if (health > 0)
                PrepareNextMove();
        }
        else
        {
            currState = BossState.Idle;
        }

    }

    private void Move(float s)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, s * Time.deltaTime);
    }

    private bool IsInAttackRange()
    {
        return distanceFromPlayer < attackRange;
    }

    public void TakeDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        healthBarSlider.value = CalculateHealthPercentage();
        CheckDeath();
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            currState = BossState.Death;
            healthBar.SetActive(false);
            anim.SetBool("IsDead", true);
            Destroy(gameObject, 0.5f);
        }
    }

    private void PrepareNextMove()
    {
        if (Time.time > nextAttackTime)
        {
            if (IsInAttackRange())
            {
                damagedPlayer = false;
                currState = BossState.MeleeAttack;
            }
            else
            {
                int r = 0;
                r = Random.Range(1, 3);

                if (r == 1)
                {
                    currState = BossState.Move;
                }
                else if (r == 2)
                {
                    attackedPlayer = false;
                    currState = BossState.RangeAttack;
                }
                else
                {
                    currState = BossState.Move;
                }

            }
            nextAttackTime = Time.time + timeBetweenAttacks;
        }
    
    }

    IEnumerator MeleeAttack()
    {
        anim.SetBool("IsAttacking", true);
        
        yield return new WaitForSeconds(0.6f);
        if (!damagedPlayer && IsInAttackRange())
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(80f);
            damagedPlayer = true;
        }

        anim.SetBool("IsAttacking", false);
        currState = BossState.Move;
    }

    IEnumerator OrbAttack()
    {
        anim.SetBool("IsShooting", true);
              
        yield return new WaitForSeconds(0.6f);

        if (!attackedPlayer)
        {
            Instantiate(bullet[0], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate(bullet[0], new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
            Instantiate(bullet[0], new Vector2(transform.position.x, transform.position.y - 1), Quaternion.identity);
            Instantiate(bullet[0], new Vector2(transform.position.x - 1, transform.position.y + 1), Quaternion.identity);
            Instantiate(bullet[0], new Vector2(transform.position.x - 1, transform.position.y - 1), Quaternion.identity);
            attackedPlayer = true;
        }

        anim.SetBool("IsShooting", false);
        currState = BossState.Move;
    }
}

    
