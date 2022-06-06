using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProjectile : MonoBehaviour
{

    public float damage;
    private float delay = 1;


    void WaitAndDestroy()
    {
        Destroy(gameObject, delay);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Wall" | collision.tag == "Door")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Enemy" | collision.tag == "Boss")
        {
            if (collision.GetComponent<EnemyController>() != null)
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
