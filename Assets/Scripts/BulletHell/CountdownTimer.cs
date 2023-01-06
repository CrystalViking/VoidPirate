using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


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
        if(sceneInfo.isEventOn == false)
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
            DestroyAllComponents();
            sceneInfo.isEventOn = false;
            PlayerPrefs.DeleteKey("shipHealth");
            StartCoroutine(SceneLoader.instance.LoadScene("LobbyShipFinal"));
        }
    }

}
