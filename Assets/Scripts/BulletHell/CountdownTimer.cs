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
    public float startingTime = 10f;
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
        timerSO.Value -= 1 * Time.deltaTime;
        countdownText.text = timerSO.Value.ToString("F0");

        if(timerSO.Value <= 0)
        {
            timerSO.Value = 0;
            DestroyAllComponents();
            SceneManager.LoadScene("EndingScene");
        }

        if(timerSO.Value <= 40 & timerSO.Value > 39)
        {
            timerSO.Value = 39;
            sceneInfo.isEventOn = true;
            DestroyAllComponents();
            SceneManager.LoadScene("LobbyShip");
        }
    }

}
