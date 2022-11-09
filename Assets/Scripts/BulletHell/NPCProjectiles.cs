using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCProjectiles : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bullet;
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Enemy");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bullet.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag != "Player")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyReceiveDamage>().TakeDamage(15);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5);
        }
    }
}

