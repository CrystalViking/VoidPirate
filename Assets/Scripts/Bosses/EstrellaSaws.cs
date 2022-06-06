using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstrellaSaws : MonoBehaviour
{
    public GameObject target;
    public float speed;
    public float x = 0;
    public float y = 0;
    Rigidbody2D bullet;
    void Start()
    {
        bullet = GetComponent<Rigidbody2D>();
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bullet.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag != "Boss" && collision.tag != "Projectile" && collision.tag != "PlayerProjectile" && collision.name != "Player")
        {
            Destroy(gameObject);
        }
        else if(collision.name == "Player")
        {
            collision.GetComponent<PlayerStats>().TakeDamage(25);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject, 10);
        }
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 180f;
        if (n < 0) n += 360;

        return n;
    }

    public void SetGameObject(GameObject x)
    {
        target = x;
    }
}
