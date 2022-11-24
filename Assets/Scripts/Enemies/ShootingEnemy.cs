using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    public float speed;
    public float lineOfSight;
    public float shootingRange;
    public GameObject bullet;
    public float timeBetweenShots;
    private float nextShotTime;
    private Transform player;
    private Animator anim;
    public AudioSource audioSource;
    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            anim.SetBool("IsAttacking", false);
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange && Time.time > nextShotTime)
        {
            anim.SetBool("IsAttacking", true);
            Instantiate(bullet, transform.position, Quaternion.identity);
            audioSource.Play();
            nextShotTime = Time.time + timeBetweenShots;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
