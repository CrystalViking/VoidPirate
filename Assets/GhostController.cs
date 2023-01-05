using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] public float damage;
    [SerializeField] public float tick;
    [SerializeField] public float speed;
    private GameObject player;
    private float nextAttackTime;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        nextAttackTime = tick;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if(Vector2.Distance(transform.position, player.transform.position) < 0.5)
        {
            if (Time.time > nextAttackTime)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().TakeDamage(damage);
                nextAttackTime = Time.time + tick;
            }

        }
    }

    public void Banish()
    {
        Destroy(gameObject);
    }
}
