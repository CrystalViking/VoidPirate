using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;


public class CountdownTimer : MonoBehaviour
{
    [SerializeField]
    private FloatSO timerSO;
    [SerializeField]
    private SceneInfo sceneInfo;
    public float startingTime = 60.0f;
    private float increasingTimer;
    GameObject[] enemies;
    GameObject[] projectiles;
    [SerializeField] private GameObject loadingScreen;
    PShipHealth healthScript;

    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public string levelToLoad;
    void DestroyAllComponents()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        projectiles = GameObject.FindGameObjectsWithTag("EnemyProjectile");
        if ((gameObject.GetComponent("BulletHellEnemySpawner") as BulletHellEnemySpawner) != null)
        {
            GetComponent<BulletHellEnemySpawner>().enabled = false;
        }
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        foreach (GameObject projectile in projectiles)
        {
            Destroy(projectile);
        }
    }
    [SerializeField] TextMeshProUGUI countdownText;
    void Start()
    {
        healthScript = GetComponent<PShipHealth>();
        if (sceneInfo.isEventOn == false)
        {
            timerSO.Value = startingTime;
        }

    }

    private void Update()
    {
        increasingTimer += Time.deltaTime;
        timerSO.Value -= Time.deltaTime;
        countdownText.text = timerSO.Value.ToString("F0");

        if (timerSO.Value <= 0f)
        {
            timerSO.Value = 0f;
            healthScript.canBeAttacked = false;
            DestroyAllComponents();
            sceneInfo.isEventOn = false;
            PlayerPrefs.DeleteKey("shipHealth");
            //LoadLevelButton(levelToLoad);
            FindObjectOfType<AsyncLoader>().LoadLevel(levelToLoad);
        }
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
}