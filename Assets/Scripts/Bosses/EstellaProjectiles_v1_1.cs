using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstellaProjectiles_v1_1 : BaseEnemyProjectile
{
    void Start()
    {
        InitProjectile();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTriggerBehavior(collision);
    }

    public override void InitProjectile()
    {
        base.InitProjectile();

        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bullet.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(target.transform.position - transform.position));
        bullet.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    public override void OnTriggerBehavior(Collider2D collision)
    {
        if (!(collision.CompareTag("Enemy") ||
            collision.CompareTag("Boss") || 
            collision.CompareTag("Projectile") || 
            collision.CompareTag("Player") ||
            collision.CompareTag("Untagged")))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, projectileDestroyTime);
        }
    }


    


}
