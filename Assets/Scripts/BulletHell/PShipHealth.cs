using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PShipHealth : MonoBehaviour
{
    [SerializeField]
    private SceneInfo sceneInfo;
    [SerializeField] public float maxHealth = 500;
    public float health;
    private bool isDead;
    private float dmgTaken;

    private void Start()
    {
        dmgTaken = 0;
        isDead = false;
        if (sceneInfo.isEventOn == false)
        {
            health = maxHealth;
            PlayerPrefs.SetFloat("shipHealth", health);
        }
        else
        {
            health = PlayerPrefs.GetFloat("shipHealth", maxHealth);
        }
        
    }

    private void Update()
    {
        if (dmgTaken >= 300)
        {
            dmgTaken = 0;
            ScreenShakeController.instance.StartShake(1f, 3f);
            sceneInfo.isEventOn = true;
            StartCoroutine(SceneLoader.instance.LoadScene("LobbyShipEvent"));
        }
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
            PlayerPrefs.DeleteKey("shipHealth");
            isDead = true;
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        if(!isDead)
        {
            health -= damage;
            dmgTaken += damage;
            PlayerPrefs.SetFloat("shipHealth", health);
            CheckDeath();
        }
        
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }

    public void Die()
    {
        sceneInfo.isEventOn = false;
        Debug.Log("is Dead");
        SceneManager.LoadScene("GameOverScene");

    }
}
