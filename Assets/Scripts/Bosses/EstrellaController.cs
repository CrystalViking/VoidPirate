using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstrellaController : MonoBehaviour
{
    public enum BossState
    {
        Idle,
        Move,
        SawAttack,
        FlameAttack,
        Debuff,
        MeleeAttack,
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
    public GameObject[] sawLocators;
    private bool shouldDebuff = true;
    private bool damagedPlayer = false;
    private float playerspeed;
    float distanceFromPlayer;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        nextAttackTime = 1.0f;
        playerspeed = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().GetSpeed();
    }

    void Update()
    {
        switch (currState)
        {

            case (BossState.Idle):
                break;
            case (BossState.Move):
                Move();
                break;
            case (BossState.SawAttack):
                SawAttackPrep();
                break;
            case (BossState.FlameAttack):
                StartCoroutine(FlameAttack());
                break;
            case (BossState.Debuff):
                StartCoroutine(Debuff());
                break;
            case (BossState.MeleeAttack):
                StartCoroutine(MeleeAttack());
                break;
            case (BossState.Death):
                // Death();
                break;
        }

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (isInRoom)
        {
            if(health > 0)
                PrepareNextMove();
        }
        else
        {
            currState = BossState.Idle;
        }

    }

    public void TakeDamage(float damage)
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
            currState = BossState.Death;
            healthBar.SetActive(false);
            anim.SetBool("IsDead", true);
            Destroy(gameObject, 10f);
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

    private bool AttackFromLeftSide()
    {
        if (player.position.x <= transform.position.x)
        {
            return true;
        }
        else
            return false;
    }

    IEnumerator MeleeAttack()
    {
        anim.SetBool("IsMeleeing", true);
        if (AttackFromLeftSide())
        {
            anim.SetBool("IsAttackingLeft", true);
        }
        else
        {
            anim.SetBool("IsAttackingLeft", false);
        }
        if (!damagedPlayer)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(30f);
            damagedPlayer = true;
        }
        yield return new WaitForSeconds(0.6f);
        
        anim.SetBool("IsMeleeing", false);
        currState = BossState.Move;
    }

    private void SawAttackPrep()
    {
        GameObject[] sawprojectiles = new GameObject[8];
        for (int i = 0; i < 8; i++)
        {
            sawprojectiles[i] = Instantiate(bullet[0], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            //sawprojectiles[i].GetComponent<EstrellaSaws>().SetGameObject(sawLocators[i]);
            sawprojectiles[i].GetComponent<EstrellaSaws_v1_1>().SetGameObject(sawLocators[i]);
        }
        currState = BossState.Idle;
        StartCoroutine(SawAttack());
    }
    IEnumerator SawAttack()
    {
        anim.SetBool("IsSawAttacking", true);

        yield return new WaitForSeconds(0.6f);
        
        anim.SetBool("IsSawAttacking", false);
        currState = BossState.Move;
    }

    IEnumerator FlameAttack()
    {
        anim.SetBool("IsFlameAttacking", true);
        if (AttackFromLeftSide())
        {
            anim.SetBool("IsAttackingLeft", true);
            GameObject flameprojectile = Instantiate(bullet[1], new Vector2(transform.position.x - 0.5f, transform.position.y), Quaternion.identity);
        }
        else
        {
            anim.SetBool("IsAttackingLeft", false);
            GameObject flameprojectile = Instantiate(bullet[1], new Vector2(transform.position.x + 0.5f, transform.position.y), Quaternion.identity);
        }
        yield return new WaitForSeconds(0.4f);
        anim.SetBool("IsFlameAttacking", false);
        currState = BossState.Move;
    }

    IEnumerator Debuff()
    {
        anim.SetBool("IsDebuffing", true);
        
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().SetSpeed(playerspeed/2.0f);

        StartCoroutine(DebuffTime());

        yield return new WaitForSeconds(1.2f);

        anim.SetBool("IsDebuffing", false);
        shouldDebuff = false;
        currState = BossState.Move;
    }

    IEnumerator DebuffTime()
    {
        yield return new WaitForSeconds(10.0f);
        StartCoroutine(DebuffCooldown());
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().SetSpeed(playerspeed);

    }

    IEnumerator DebuffCooldown()
    {
        yield return new WaitForSeconds(20.0f);
        shouldDebuff = true;
    }
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
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
                if (shouldDebuff)
                    r = Random.Range(1, 5);
                else
                    r = Random.Range(1, 4);

                if (r == 1)
                {
                    currState = BossState.Move;
                }
                else if(r == 2)
                {
                    currState = BossState.SawAttack;
                }
                else if(r == 3)
                {
                    currState = BossState.FlameAttack;
                }
                else if (r == 4)
                {
                    currState = BossState.Debuff;
                }
                else
                {
                    currState = BossState.Move;
                }

            }
            nextAttackTime = Time.time + timeBetweenAttacks;
        }
        

    }

    // private void Death()
    // {
    //     isDead = true;
    // }

}
