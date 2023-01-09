using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class PShipHealth : MonoBehaviour
{
    [SerializeField]
    private SceneInfo sceneInfo;
    [SerializeField] public float maxHealth = 500;
    public float health;
    private bool isDead;
    private float dmgTaken;

    private int healthSetOnStart = 0;

    [SerializeField]
    private UnityEvent<float> sliderOnHealthChanged;
    [SerializeField]
    private UnityEvent<string> textOnHealthChanged;

    [Header("Sound Effects")]
    public AudioSource audioSource;

    private void Start()
    {
        dmgTaken = 0;
        isDead = false;
        if (sceneInfo.isEventOn == false)
        {
            if(healthSetOnStart == 0)
                health = maxHealth;

            healthSetOnStart = 1;

            PlayerPrefs.SetFloat("shipHealth", health);
            PlayerPrefs.SetFloat("maxHealth", health);
            PlayerPrefs.SetInt("healthSetOnStart", 1);
        }
        else
        {
            health = PlayerPrefs.GetFloat("shipHealth", health);
            maxHealth = PlayerPrefs.GetFloat("maxHealth", maxHealth);
            healthSetOnStart = PlayerPrefs.GetInt("healthSetOnStart", healthSetOnStart);

        }
        textOnHealthChanged?.Invoke(health.ToString());
        sliderOnHealthChanged.Invoke(CalculateHealthPercentage());
    }

    private void Update()
    {
        if (dmgTaken >= 300)
        {
            dmgTaken = 0;
            ScreenShakeController.instance.StartShake(1f, 3f);
            sceneInfo.isEventOn = true;
            StartCoroutine(SceneLoader.instance.LoadScene("LobbyShipFinal"));
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

    public float GetHealth()
    {
        return health;
    }

    public void TakeDamage(float damage)
    {
        if(!isDead)
        {
            audioSource.Play();
            health -= damage;
            dmgTaken += damage;
            PlayerPrefs.SetFloat("shipHealth", health);

            sliderOnHealthChanged?.Invoke(CalculateHealthPercentage());
            textOnHealthChanged?.Invoke(health.ToString());

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
        PlayerPrefs.SetInt("coinAmount", 0);
        Debug.Log("is Dead");
        StartCoroutine(SceneLoader.instance.LoadScene("LobbyShipFinal"));

    }
}
