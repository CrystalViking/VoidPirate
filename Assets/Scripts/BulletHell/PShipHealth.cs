using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using TMPro;

public class PShipHealth : MonoBehaviour, IDataPersistence
{
    [SerializeField]
    private SceneInfo sceneInfo;
    [SerializeField] public float maxHealth = 500;
    public float health;
    private bool isDead;
    private float dmgTaken;
    public TMP_Text warningText;
    public string levelToLoad;

    private int healthSetOnStart = 0;

    [SerializeField]
    private UnityEvent<float> sliderOnHealthChanged;
    [SerializeField]
    private UnityEvent<string> textOnHealthChanged;

    [SerializeField] private GameObject loadingScreen;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;


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
        if (dmgTaken >= 250)
        {
            warningText.gameObject.SetActive(true);
            ScreenShakeController.instance.StartShake(2f, 3f);
        }
        if (dmgTaken >= 300)
        {
            dmgTaken = 0;
            //ScreenShakeController.instance.StartShake(2f, 3f);

            sceneInfo.isEventOn = true;
            LoadLevelButton(levelToLoad);
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

        DataPersistenceManager.instance.LoadGame();
        DataPersistenceManager.instance.SaveGame();

        LoadLevelButton(levelToLoad);

    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(GameData data)
    {
        data.lobbyTravelState = LobbyTravelState.ReadyToTravel;
        data.lobbyBossState = LobbyBossState.bossLocationUnknown;
    }
    public void LoadLevelButton(string levelToLoad)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadLevelASync(levelToLoad));
    }
    IEnumerator LoadLevelASync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
    }
}
