using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseEnemyProjectile : MonoBehaviour
{   
    [SerializeField] public ScriptableProjectile projectile = null;
    protected GameObject target;
    protected Rigidbody2D bullet;
    protected float damage;
    protected float speed;


    protected float projectileDestroyTime;

   
    
    public virtual void OnTriggerBehavior(Collider2D collision)
    {
        if (!(collision.CompareTag("Enemy") ||
            collision.CompareTag("Player")))
        {
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(projectile.damage);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, projectileDestroyTime);
        }
    }

    private void GetReferences()
    {
        
    }

    public virtual void InitProjectile()
    {
        projectileDestroyTime = projectile.projectileDestroyTime;
        bullet = GetComponent<Rigidbody2D>();
        speed = projectile.speed;
        damage = projectile.damage;
    }


    

}
