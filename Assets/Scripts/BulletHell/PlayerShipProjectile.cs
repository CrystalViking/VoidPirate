using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipProjectile : MonoBehaviour
{

    [SerializeField] public float damage;
    private float delay = 1;


    void WaitAndDestroy()
    {
        Destroy(gameObject, delay);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Wall" || collision.tag == "Door")
        {
            Destroy(gameObject);
        }

        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {

            collision.GetComponent<EnemyReceiveDamage>().TakeDamage(damage);

            Destroy(gameObject);
        }
        else
        {
            WaitAndDestroy();
        }
    }
}
