using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PShipHealth : MonoBehaviour
{
    
    [SerializeField] public float maxHealth = 500;
    public float health;
    private bool isDead;

    private void Start()
    {
        InitVariables();
    }

    private void Update()
    {
        CheckDeath();

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            TakeDamage(20f);
        }
    }


    public void CheckDeath()
    {
        if(health <= 0)
        {
            health = 0;
            isDead = true;
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if(!isDead)
        {
            health -= damage;
            CheckDeath();
        }
        
    }

    private void InitVariables()
    {
        isDead = false;
        health = maxHealth;
    }
    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    public void Die()
    {
        Debug.Log("is Dead");
        SceneManager.LoadScene("GameOverScene");

    }
}
