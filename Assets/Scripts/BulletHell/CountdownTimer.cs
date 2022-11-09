using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class CountdownTimer : MonoBehaviour
{
    public float currentTime = 0f;
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
        currentTime = startingTime;
    }
    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("F0");

        if(currentTime <= 0)
        {
            currentTime = 0;
            DestroyAllComponents();
            SceneManager.LoadScene("EndingScene");
        }
    }

}
