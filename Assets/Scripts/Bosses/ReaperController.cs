using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReaperController : Enemy, IEnemy
{
    /*public enum EnemyState
    {
        Idle,
        Move,
        MeleeAttack,
        RangeAttack,
        Spawn,
        Consume,
        Teleport,
        Die

    }*/

    public float speed;
    public float attackRange;
    public GameObject[] bullet;
    public float timeBetweenAttacks;
    //private float nextAttackTime;
    private Transform player;
    private Animator anim;
    //public float health;
    public float maxHealth;
    //public GameObject healthBar;
    //public Slider healthBarSlider;
    //public EnemyState currState = EnemyState.Idle;
    //public bool isInRoom = false;
    //public bool isDead = false;
    private bool damagedPlayer = false;
    private bool attackedPlayer = false;
    private bool spawnedMinion = false;
    private bool sacrificedMinion = false;
    float distanceFromPlayer;
    public GameObject minion;
    public List<GameObject> minion_list;
    public GameObject portal;
    private bool isSpawned = false;
    public AudioSource[] audioSources;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        nextAttackTime = 1.0f;
        useRoomLogic = true;
        healthBar = GetComponent<HealthBar>();
    }

    void Update()
    {
        switch (currState)
        {

            case (EnemyState.Idle):
                break;
            case (EnemyState.Move):
                Move(speed);
                break;
            case (EnemyState.MeleeAttack):
                Move(speed / 2f);
                StartCoroutine(MeleeAttack());
                break;
            case (EnemyState.RangeAttack):
                StartCoroutine(OrbAttack());
                break;
            case (EnemyState.Spawn):
                Move(speed / 2f);
                StartCoroutine(SpawnMinion());
                break;
            case (EnemyState.Consume):
                Move(speed / 2f);
                StartCoroutine(ConsumeMinion());
                break;
            case (EnemyState.Teleport):
                StartCoroutine(Teleport());
                break;
            case (EnemyState.Die):
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

    private void Move(float s)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, s * Time.deltaTime);
    }

    private bool IsInAttackRange()
    {
        return distanceFromPlayer < attackRange;
    }

    public new void TakeDamage(float damage)
    {
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

            if (useRoomLogic)
                RoomController.instance.StartCoroutine(RoomController.instance.RoomCorutine());

            Destroy(gameObject, 0.5f);
        }
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    private void PrepareNextMove()
    {
        if (Time.time > nextAttackTime)
        {
            if (IsInAttackRange())
            {
                damagedPlayer = false;
                currState = EnemyState.MeleeAttack;
            }
            else
            {
                int r = 0;
                r = Random.Range(1, 10);

                if (r == 2 || r == 7 || r == 1)
                {
                    attackedPlayer = false;
                    currState = EnemyState.RangeAttack;
                }
                else if (r == 3 || r == 8)
                {
                    spawnedMinion = false;
                    currState = EnemyState.Spawn;
                }
                else if (r == 4 || r == 9 || r == 6)
                {
                    sacrificedMinion = false;
                    currState = EnemyState.Consume;
                }
                else if (r == 5)
                {                 
                    currState = EnemyState.Teleport;
                }
                else
                {
                    currState = EnemyState.Teleport;
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
            audioSources[1].Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(80f);
            damagedPlayer = true;
        }

        anim.SetBool("IsAttacking", false);
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Die;
    }

    IEnumerator OrbAttack()
    {
        anim.SetBool("IsShooting", true);

        yield return new WaitForSeconds(0.6f);

        if (!attackedPlayer)
        {
            audioSources[0].Play();
            Instantiate(bullet[0], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            Instantiate(bullet[0], new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
            Instantiate(bullet[0], new Vector2(transform.position.x, transform.position.y - 1), Quaternion.identity);
            Instantiate(bullet[0], new Vector2(transform.position.x - 1, transform.position.y + 1), Quaternion.identity);
            Instantiate(bullet[0], new Vector2(transform.position.x - 1, transform.position.y - 1), Quaternion.identity);
            attackedPlayer = true;
        }

        anim.SetBool("IsShooting", false);
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Die;
    }

    IEnumerator SpawnMinion()
    {
        anim.SetBool("IsSpawning", true);

        yield return new WaitForSeconds(0.6f);

        if (!spawnedMinion)
        {
            audioSources[2].Play();
            minion_list.Add(Instantiate(minion, transform.position, Quaternion.identity));
            minion_list.Add(Instantiate(minion, new Vector2(transform.position.x + 0.5f, transform.position.y + 0.5f), Quaternion.identity));

            spawnedMinion = true;
        }

        anim.SetBool("IsSpawning", false);
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Die;
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
                    audioSources[3].Play();
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
            currState = EnemyState.Move;
            if (health < 0)
                currState = EnemyState.Die;
        }
        else
        {
            yield return new WaitForSeconds(0.1f);

            currState = EnemyState.Move;
            if (health < 0)
                currState = EnemyState.Die;
        }
    }

    IEnumerator Teleport()
    {
        anim.SetBool("IsTeleporting", true);
        audioSources[4].Play();

        yield return new WaitForSeconds(0.2f);

        transform.position = player.position;
        

        anim.SetBool("IsTeleporting", false);
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Die;
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

}

    
