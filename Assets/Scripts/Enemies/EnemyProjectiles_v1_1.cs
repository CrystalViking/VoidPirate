using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectiles_v1_1 : BaseEnemyProjectile
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

        target = GameObject.FindGameObjectWithTag("Player");
        Vector2 moveDir = (target.transform.position - transform.position).normalized * speed;
        bullet.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(target.transform.position - transform.position));
        bullet.velocity = new Vector2(moveDir.x, moveDir.y);
    }

    public override void OnTriggerBehavior(Collider2D collision)
    {
        base.OnTriggerBehavior(collision);
    }
}
