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
        Debug.Log(collision.gameObject.layer.ToString());
        Debug.Log(collision.tag.ToString());

        Collider2D tempcol = collision;


        if (!(collision.CompareTag("Enemy") || 
            collision.CompareTag("Player") || 
            collision.gameObject.layer == LayerMask.NameToLayer("Floor") ||
            collision.CompareTag("Untagged")))
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

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
        if (n < 0) n += 360;

        return n;
    }




}
