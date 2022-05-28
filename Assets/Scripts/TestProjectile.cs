using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{
    
    public float damage;
    private float delay = 5;


    void WaitAndDestroy()
    {
        Destroy(gameObject, delay);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player" && collision.tag != "EnemyProjectile")
        {
            if(collision.GetComponent<EnemyReceiveDamage>() != null)
            {
                collision.GetComponent<EnemyReceiveDamage>().DealDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            WaitAndDestroy();
        }
    }
}
