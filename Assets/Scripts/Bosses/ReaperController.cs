using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReaperController : MonoBehaviour, IEnemy
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
        Die

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
    private bool spawnedMinion = false;
    private bool sacrificedMinion = false;
    float distanceFromPlayer;
    public GameObject minion;
    public List<GameObject> minion_list;
    public GameObject portal;
    private bool isSpawned = false;

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
                Move(speed / 2f);
                StartCoroutine(MeleeAttack());
                break;
            case (BossState.RangeAttack):
                StartCoroutine(OrbAttack());
                break;
            case (BossState.Spawn):
                Move(speed / 2f);
                StartCoroutine(SpawnMinion());
                break;
            case (BossState.Consume):
                Move(speed / 2f);
                StartCoroutine(ConsumeMinion());
                break;
            case (BossState.Teleport):
                StartCoroutine(Teleport());
                break;
            case (BossState.Die):
                StartCoroutine(SpawnPortal());
                break;
        }

        foreach(GameObject m in minion_list)
        {
            if (!m)
            {
                minion_list.Remove(m);
                break;
            }
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
        if (health > maxHealth)
            health = maxHealth;
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
            currState = BossState.Die;
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
                r = Random.Range(1, 10);

                if (r == 2 || r == 7 || r == 1)
                {
                    attackedPlayer = false;
                    currState = BossState.RangeAttack;
                }
                else if (r == 3 || r == 8)
                {
                    spawnedMinion = false;
                    currState = BossState.Spawn;
                }
                else if (r == 4 || r == 9 || r == 6)
                {
                    sacrificedMinion = false;
                    currState = BossState.Consume;
                }
                else if (r == 5)
                {                 
                    currState = BossState.Teleport;
                }
                else
                {
                    currState = BossState.Teleport;
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
        if (health < 0)
            currState = BossState.Die;
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
        if (health < 0)
            currState = BossState.Die;
    }

    IEnumerator SpawnMinion()
    {
        anim.SetBool("IsSpawning", true);

        yield return new WaitForSeconds(0.6f);

        if (!spawnedMinion)
        {
            minion_list.Add(Instantiate(minion, transform.position, Quaternion.identity));
            minion_list.Add(Instantiate(minion, new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f), Quaternion.identity));

            spawnedMinion = true;
        }

        anim.SetBool("IsSpawning", false);
        currState = BossState.Move;
        if (health < 0)
            currState = BossState.Die;
    }

    IEnumerator ConsumeMinion()
    {
        if (minion_list.Count > 0)
        {
            anim.SetBool("IsConsuming", true);

            yield return new WaitForSeconds(0.8f);

            if (!sacrificedMinion)
            {
                try
                {
                    minion_list[0].GetComponent<MinionController>().Sacrifice();
                    TakeDamage(-500.0f);
                    sacrificedMinion = true;
                }
                catch
                {
                    TakeDamage(-1.0f);
                    sacrificedMinion = true;
                }
            }

        

            anim.SetBool("IsConsuming", false);
            currState = BossState.Move;
            if (health < 0)
                currState = BossState.Die;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);

            currState = BossState.Move;
            if (health < 0)
                currState = BossState.Die;
        }
    }

    IEnumerator Teleport()
    {
        anim.SetBool("IsTeleporting", true);

        yield return new WaitForSeconds(0.2f);

        transform.position = player.position;

        anim.SetBool("IsTeleporting", false);
        currState = BossState.Move;
        if (health < 0)
            currState = BossState.Die;
    }

    IEnumerator SpawnPortal()
    {
        if (!isSpawned)
        {
            Instantiate(portal, transform.position, Quaternion.identity);
            isSpawned = true;
        }
        yield return new WaitForSeconds(0.5f);
    }

    public void SetUseRoomLogicTrue()
    { }
    public void SetUseRoomLogicFalse()
    { }

    public void SetActiveBehaviourTrue()
    { }
    public void SetActiveBehaviourFalse()
    { }

    public EnemyState GetEnemyState()
    {
        return EnemyState.Idle;
    }

    public bool HasFullHealth()
    {
        if (health == maxHealth)
            return true;
        else
            return false;
    }
}

    
