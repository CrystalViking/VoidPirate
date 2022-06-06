using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : ActorStats
{
    [SerializeField] ScriptableEnemy enemy;
    private float lineOfSight;
    private float attackRange;
   
   

    public void Die(float despawnTime = 10f)
    {
        base.Die();
        Destroy(gameObject, despawnTime);
    }



    public override void InitVariables(float maxHealth = 25f)
    {
        SetHealthTo(maxHealth);
        isDead = false;

        
    }

    // Start is called before the first frame update
    void Start()
    {
        InitVariables();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitVariables()
    {
        speed = enemy.speed;
        lineOfSight = enemy.lineOfSight;
        attackRange = enemy.attackRange;
        
    }
}
