using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtarosController : Enemy, IEnemy
{
    public float speed;
    public float attackRange;
    public GameObject bullet;
    public float timeBetweenAttacks;
    private Transform player;
    private Animator anim;
    public float maxHealth;
    public bool isDead = false;
    private bool damagedPlayer = false;
    float distanceFromPlayer;
    public GameObject portal;
    private bool isSpawned = false;
    public GameObject[] orbLocators;
    public AudioSource[] audioSources;
    public ParticleSystem particles;
    public GameObject cashParticles;
    public GameObject[] orbDestinators;
    private bool moneySpawned;
    private bool isReborned;
    private bool isReborning = false;
    public GameObject[] demons;
    private bool orbingI = true;
    private bool orbingII = true;
    private bool orbingIII = true;
    private GameObject redDemon;
    private GameObject blueDemon;
    public GameObject bolt;
    public bool spawned = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        health = maxHealth;
        nextAttackTime = 1.0f;
        useRoomLogic = true;
        healthBar = GetComponent<HealthBar>();
        isReborned = false;
    }

    void Update()
    {
        switch (currState)
        {

            case (EnemyState.Idle):
                StartCoroutine(Wait());
                break;
            case (EnemyState.Move):
                Move();
                break;
            case (EnemyState.MoveReborned):
                Move(speed * 2f);
                break;
            case (EnemyState.MeleeAttack):
                Move(speed / 4f);
                StartCoroutine(MeleeAttack());
                break;
            case (EnemyState.MeleeAttackReborned):
                Move(speed / 3f);
                StartCoroutine(MeleeAttackReborned());
                break;
            case (EnemyState.RangeAttack):
                StartCoroutine(OrbingI());
                StartCoroutine(OrbingII());
                StartCoroutine(OrbingIII());
                StartCoroutine(OrbAttack());
                break;
            case (EnemyState.Die):
                StartCoroutine(SpawnPortal());
                break;
        }

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (activeBehaviour)
        {
                PrepareNextMove();
        }
        else
        {
            currState = EnemyState.Idle;
        }

        if(redDemon)
        {
            redDemon.transform.RotateAround(transform.position, Vector3.forward, 30 * Time.deltaTime);
            redDemon.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
            
        }
        if (blueDemon)
        {
            blueDemon.transform.RotateAround(transform.position, Vector3.forward, 30 * Time.deltaTime);
            blueDemon.transform.rotation = new Quaternion(0.0f, 0.0f, 0.0f, 0.0f);
        }
        if(isReborned && !blueDemon && !redDemon)
        {
            DieRightNow();
        }

    }

    public new void TakeDamage(float damage)
    {
        if (!isReborned)
        {
            if (!particles.isPlaying)
                particles.Play();
            healthBar.SetHealthBarActive();
            health -= damage;
            healthBar.SetHealthBarValue(CalculateHealthPercentage());
            CheckDeath();
        }
        else
        {
            Instantiate(bolt, transform.position, Quaternion.identity);
        }
    }

    private new void CheckDeath()
    {
        if (health <= 0)
        {
            if (!isReborned)
            {
                StartCoroutine(StartReborning());
                StartCoroutine(SpawnDemons());
                currState = EnemyState.Reborn;
            }           
            
        }
    }
    private  void DieRightNow()
    {
        currState = EnemyState.Die;
        anim.SetBool("IsDead", true);

            if (useRoomLogic)
                RoomController.instance.StartCoroutine(RoomController.instance.RoomCorutine());
           
            Destroy(gameObject, 1.5f);
    }

    IEnumerator StartReborning()
    {
        anim.SetBool("IsReborning", true);
        isReborning = true;

        yield return new WaitForSeconds(5.3f);

        anim.SetBool("IsReborning", false);
        anim.SetBool("IsReborned", true);
        isReborned = true;
        currState = EnemyState.MoveReborned;
    }

    IEnumerator SpawnDemons()
    {
        yield return new WaitForSeconds(4.3f);
        if (!spawned)
        {
            redDemon = Instantiate(demons[0], new Vector2(transform.position.x - 2, transform.position.y), Quaternion.identity);
            blueDemon = Instantiate(demons[1], new Vector2(transform.position.x + 2, transform.position.y), Quaternion.identity);
            spawned = true;
        }

        redDemon.transform.parent = transform;
        blueDemon.transform.parent = transform;
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    private bool IsInAttackRange()
    {
        return distanceFromPlayer < attackRange;
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
    }

    private void Move(float s)
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, s * Time.deltaTime);
    }

    private void PrepareNextMove()
    {
        if (Time.time > nextAttackTime)
        {
            if (!isReborned)
            {
                if (IsInAttackRange())
                {
                    damagedPlayer = false;
                    currState = EnemyState.MeleeAttack;
                }
                else
                {
                    int r = 0;

                    r = Random.Range(2, 3);

                    if (r == 1)
                    {
                        currState = EnemyState.Move;
                    }
                    else
                    if (r == 2)
                    {
                        orbingI = true;
                        orbingII = true;
                        orbingIII = true;
                        currState = EnemyState.RangeAttack;
                    }
                    else
                    {
                        currState = EnemyState.Move;
                    }

                }
            }
            else
            {
                if (IsInAttackRange())
                {
                    damagedPlayer = false;
                    currState = EnemyState.MeleeAttackReborned;
                }
                else
                {
                    int rR = 0;

                    rR = Random.Range(1, 4);

                    if (rR == 1)
                    {
                        currState = EnemyState.MoveReborned;
                    }
                    else if (rR == 2)
                    {
                        currState = EnemyState.MoveReborned;
                    }
                    else if (rR == 3)
                    {
                        currState = EnemyState.Idle;
                    }
                    else
                    {
                        currState = EnemyState.Idle;
                    }
                }
            }
            nextAttackTime = Time.time + timeBetweenAttacks;
        }


    }

    IEnumerator MeleeAttack()
    {
        anim.SetBool("IsMeleeing", true);

        yield return new WaitForSeconds(0.333f);
        if (!damagedPlayer && IsInAttackRange())
        {
            audioSources[0].Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(70f);
            damagedPlayer = true;
        }

        anim.SetBool("IsMeleeing", false);
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Reborn;
    }

    IEnumerator MeleeAttackReborned()
    {
        anim.SetBool("IsMeleeing", true);

        yield return new WaitForSeconds(0.333f);
        if (!damagedPlayer && IsInAttackRange())
        {
            audioSources[0].Play();
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(80f);
            damagedPlayer = true;
        }

        anim.SetBool("IsMeleeing", false);
        currState = EnemyState.MoveReborned;
       /* if (health < 0)
            currState = EnemyState.Die;*/
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.0f);
    }

    private void OrbIAttackPrep()
    {
        if (orbingI && !isReborning)
        {
            audioSources[1].Play();
            GameObject[] orbprojectiles = new GameObject[4];
            for (int i = 0; i < 4; i++)
            {
                orbprojectiles[i] = Instantiate(bullet, orbLocators[i].transform.position, Quaternion.identity);
                orbprojectiles[i].GetComponent<EstrellaSaws_v1_1>().SetGameObject(orbDestinators[i]);
            }
            orbingI = false;
        }
    }
    private void OrbIIAttackPrep()
    {
        if (orbingII && !isReborning)
        {
            audioSources[1].Play();
            GameObject[] orbprojectiles = new GameObject[4];
            for (int i = 0; i < 4; i++)
            {
                orbprojectiles[i] = Instantiate(bullet, orbLocators[i].transform.position, Quaternion.identity);
                orbprojectiles[i].GetComponent<EstrellaSaws_v1_1>().SetGameObject(orbDestinators[i]);
            }
            orbingII = false;
        }
    }
    private void OrbIIIAttackPrep()
    {
        if (orbingIII && !isReborning)
        {
            audioSources[1].Play();
            GameObject[] orbprojectiles = new GameObject[4];
            for (int i = 0; i < 4; i++)
            {
                orbprojectiles[i] = Instantiate(bullet, orbLocators[i].transform.position, Quaternion.identity);
                orbprojectiles[i].GetComponent<EstrellaSaws_v1_1>().SetGameObject(orbDestinators[i]);
            }
            orbingIII = false;
        }
    }
    IEnumerator OrbAttack()
    {
        anim.SetBool("IsOrbing", true);
        
        yield return new WaitForSeconds(1.0f); 

        anim.SetBool("IsOrbing", false);      
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Reborn;
    }

    IEnumerator OrbingI()
    {
        yield return new WaitForSeconds(0.2f);
        OrbIAttackPrep();
    }
    IEnumerator OrbingII()
    {
        yield return new WaitForSeconds(0.4f);
        OrbIIAttackPrep();
    }
    IEnumerator OrbingIII()
    {
        yield return new WaitForSeconds(0.8f);
        OrbIIIAttackPrep();
    }

    IEnumerator SpawnPortal()
    {
        yield return new WaitForSeconds(1.2f);
        if (!isSpawned)
        {
            Instantiate(portal, transform.position, Quaternion.identity);
            isSpawned = true;
        }
        if (!moneySpawned)
        {
            Instantiate(cashParticles, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
            moneySpawned = true;
        }
    }
}
