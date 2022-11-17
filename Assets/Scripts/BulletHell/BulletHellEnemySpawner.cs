using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHellEnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRadius = 7, time = 3f;

    public GameObject[] enemies;
    void Start()
    {
        StartCoroutine(SpawnAnEnemy());
    }

    IEnumerator SpawnAnEnemy()
    {
        Vector2 spawnPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnAnEnemy());
    }
}
