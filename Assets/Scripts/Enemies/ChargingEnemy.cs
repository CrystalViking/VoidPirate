using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargingEnemy : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float chargeRange;
    public float attackRange; 
    private float nextAttackTime;
    public float timeBetweenAttacks;
    private Transform player;
    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > chargeRange)
        {
            anim.SetBool("IsMoving", true);
            anim.SetBool("IsAttacking", false);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= chargeRange && distanceFromPlayer > attackRange)
        {
            anim.SetBool("IsAttacking", true);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * 2 * Time.deltaTime);
        }
        else if (distanceFromPlayer <= attackRange && Time.time > nextAttackTime)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PShipHealth>().TakeDamage(20f);
            nextAttackTime = Time.time + timeBetweenAttacks;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, chargeRange);
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
