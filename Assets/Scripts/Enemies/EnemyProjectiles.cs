using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles : MonoBehaviour
{
    GameObject target;
    public float speed;
    Rigidbody2D bullet;
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bullet.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 5);
        }
    }
}
