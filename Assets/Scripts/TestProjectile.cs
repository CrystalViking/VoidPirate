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
        if (collision.name != "Player")
        {
            if(collision.GetComponent<EnemyReceiveDamage>() != null)
            {
                collision.GetComponent<EnemyReceiveDamage>().TakeDamage(damage);
            }
            if (collision.GetComponent<EstrellaController>() != null)
            {
                collision.GetComponent<EstrellaController>().TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else
        {
            WaitAndDestroy();
        }
    }
}
