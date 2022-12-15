using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstrellaController : Enemy, IEnemy
{

    public float speed;
    public float attackRange;
    public GameObject[] bullet;
    public float timeBetweenAttacks;
    private Transform player;
    private Animator anim;
    public float maxHealth;
    public bool isDead = false;
    public GameObject[] sawLocators;
    private bool shouldDebuff = true;
    private bool damagedPlayer = false;
    float distanceFromPlayer;
    public GameObject portal;
    private bool isSpawned = false;
    public AudioSource[] audioSources;
    public ParticleSystem particles;
    public GameObject cashParticles;
    private bool moneySpawned;

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
                Move();
                break;
            case (EnemyState.SawAttack):
                SawAttackPrep();
                break;
            case (EnemyState.FlameAttack):
                StartCoroutine(FlameAttack());
                break;
            case (EnemyState.Debuff):
                StartCoroutine(Debuff());
                break;
            case (EnemyState.MeleeAttack):
                StartCoroutine(MeleeAttack());
                break;
            case (EnemyState.Die):
                StartCoroutine(SpawnPortal());
                break;
        }

        distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (activeBehaviour)
        {
            if(health > 0)
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

            if (useRoomLogic)
                RoomController.instance.StartCoroutine(RoomController.instance.RoomCorutine());

            if (!moneySpawned)
            {
                Instantiate(cashParticles, new Vector2(transform.position.x, transform.position.y + 1), Quaternion.identity);
                moneySpawned = true;
            }

            Destroy(gameObject, 1.0f); // TODO: consider using scriptable object
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
            if (!audioSources[2].isPlaying)
                audioSources[2].Play();
        }
        yield return new WaitForSeconds(0.6f); // TODO: consider using scriptable object

        anim.SetBool("IsMeleeing", false);
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Die;
    }

    private void SawAttackPrep()
    {
        GameObject[] sawprojectiles = new GameObject[8];
        for (int i = 0; i < 8; i++)
        {
            sawprojectiles[i] = Instantiate(bullet[0], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            sawprojectiles[i].GetComponent<EstrellaSaws_v1_1>().SetGameObject(sawLocators[i]);
        }
        currState = EnemyState.Idle;
        StartCoroutine(SawAttack());
    }
    IEnumerator SawAttack()
    {
        anim.SetBool("IsSawAttacking", true);
        if (!audioSources[1].isPlaying)
            audioSources[1].Play();

        yield return new WaitForSeconds(0.6f); // TODO: consider using scriptable object

        StartCoroutine(StopAudioSource1());
        anim.SetBool("IsSawAttacking", false);
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Die;
    }

    IEnumerator StopAudioSource1()
    {
        yield return new WaitForSeconds(0.7f);
        audioSources[1].Stop();
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
        audioSources[0].Play();
        yield return new WaitForSeconds(0.4f); // TODO: consider using scriptable object

        StartCoroutine(StopAudioSource0());
        anim.SetBool("IsFlameAttacking", false);
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Die;
    }

    IEnumerator StopAudioSource0()
    {
        yield return new WaitForSeconds(1.4f);
        audioSources[0].Stop();
    }

    IEnumerator Debuff()
    {
        anim.SetBool("IsDebuffing", true);
        
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>()?.
        SpeedDebuffSecondsPercentAdd(StatModApplicationType.AgentAppliedDebuff, 0.5f, 10); // TODO: consider using scriptable object

        if (!audioSources[3].isPlaying)
            audioSources[3].Play();
        StartCoroutine(DebuffCooldown());

        yield return new WaitForSeconds(1.2f); // TODO: consider using scriptable object

        anim.SetBool("IsDebuffing", false);
        shouldDebuff = false;
        currState = EnemyState.Move;
        if (health < 0)
            currState = EnemyState.Die;
    }

    IEnumerator DebuffTime()
    {
        yield return new WaitForSeconds(10.0f); // TODO: consider using scriptable object
        StartCoroutine(DebuffCooldown());

    }

    IEnumerator DebuffCooldown()
    {
        yield return new WaitForSeconds(20.0f); // TODO: consider using scriptable object
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
                currState = EnemyState.MeleeAttack;
            }
            else
            {
                int r = 0;
                if (shouldDebuff)
                    r = Random.Range(1, 5); // TODO: consider using scriptable object
                else
                    r = Random.Range(1, 4); // TODO: consider using scriptable object

                if (r == 1)
                {
                    currState = EnemyState.Move;
                }
                else if(r == 2)
                {
                    currState = EnemyState.SawAttack;
                }
                else if(r == 3)
                {
                    currState = EnemyState.FlameAttack;
                }
                else if (r == 4)
                {
                    currState = EnemyState.Debuff;
                }
                else
                {
                    currState = EnemyState.Move;
                }

            }
            nextAttackTime = Time.time + timeBetweenAttacks;
        }
        

    }

    IEnumerator SpawnPortal()
    {
        yield return new WaitForSeconds(0.7f); // TODO: consider using scriptable object
        if (!isSpawned)
        {
            Instantiate(portal, transform.position, Quaternion.identity);
            isSpawned = true;
        }
    }

}
