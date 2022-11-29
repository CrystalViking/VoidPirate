using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrellaSaws_v1_1 : BaseEnemyProjectile
{

    // Start is called before the first frame update
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

        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bullet.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    public override void OnTriggerBehavior(Collider2D collision)
    {
        if (!(collision.CompareTag("Enemy") || 
            collision.CompareTag("Boss") || 
            collision.CompareTag("Projectile") || 
            collision.CompareTag("PlayerProjectile") ||
            collision.CompareTag("Room") ||
            collision.CompareTag("Player") ||
            collision.CompareTag("Untagged")))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(damage);
            //collision.GetComponent<PlayerStats>().HealthPoison(StatModApplicationType.EntityAppliedDebuff, 10, 10);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, projectileDestroyTime);
        }
    }

    public void SetGameObject(GameObject x)
    {
        target = x;
    }
}
