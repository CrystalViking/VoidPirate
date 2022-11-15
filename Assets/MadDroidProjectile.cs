using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MadDroidProjectile : RangedEnemyProjectile
{
    new Vector2 target;
    private float X;
    private float Y;
    void Start()
    {
        X = Random.Range(-100, 100);
        Y = Random.Range(-100, 100);
        InitProjectile();
    }
    public override void InitProjectile()
    {
        base.InitProjectile();

        target = new Vector2(X, Y);
        Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
        Vector2 moveDir = (target - pos).normalized * speed;
        bullet.transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(target - pos));
        bullet.velocity = new Vector2(moveDir.x, moveDir.y);
    }
}
