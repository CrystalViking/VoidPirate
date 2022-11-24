using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyReceiveDamage : MonoBehaviour
{
    public float health;
    public float maxHealth;
    private Animator anim;
    public GameObject healthBar;
    public Slider healthBarSlider;
    public AudioSource audioSource;
    void Start()
    {
        anim = GetComponent<Animator>();
        health = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        healthBar.SetActive(true);
        health -= damage;
        healthBarSlider.value = CalculateHealthPercentage();
        CheckDeath();
    }
   
    private void CheckDeath()
    {
        if (health <= 0)
        {
            healthBar.SetActive(false);
            anim.SetBool("IsDead", true);
            if(!audioSource.isPlaying)
                audioSource.Play();
            if ((gameObject.GetComponent("FollowEnemy") as FollowEnemy) != null)
            {
                GetComponent<FollowEnemy>().enabled = false;
            }
            else if ((gameObject.GetComponent("ShootingEnemy") as ShootingEnemy) != null)
            {
                GetComponent<ShootingEnemy>().enabled = false;
            }
            else if ((gameObject.GetComponent("ChargingEnemy") as ChargingEnemy) != null)
            {
                GetComponent<ChargingEnemy>().enabled = false;
            }
            Destroy(gameObject, 1f);
        }
    }

    private float CalculateHealthPercentage()
    {
        return (health / maxHealth);
    }
}
