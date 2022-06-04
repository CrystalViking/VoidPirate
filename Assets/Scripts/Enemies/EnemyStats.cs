using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : ActorStats
{
    [SerializeField] ScriptableEnemy enemy;

    public void DealDamage(ActorStats actorStats)
    {
        // Damaging functionality
        // actorStats.TakeDamage()
    }

    public void TakeDamage()
    {
        // Taking damage functionality
    }

    public override void Die()
    {
        base.Die();
        Destroy(gameObject);
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
}
