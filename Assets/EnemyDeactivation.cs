using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyDeactivation : MonoBehaviour
{
    public List<GameObject> enemies;
    private bool canEMP;
    public float cooldownEMP;
    public float enemyDeactivationTime;
    public AudioSource audioSource;
    void Start()
    {
        canEMP = true;
    }


    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Z) && canEMP)
        {
            enemies = FindAllEnemiesInRoom();
            audioSource.Play();
            foreach (GameObject enemy in enemies)
            {
                try
                {
                    enemy.GetComponent<IEnemy>().SetActiveBehaviourFalse();
                }
                catch { }
            }
            canEMP = false;
            StartCoroutine(EMPCooldown());
            StartCoroutine(EMPDuration());
        }
    }
    IEnumerator EMPCooldown()
    {
        yield return new WaitForSeconds(cooldownEMP);
            canEMP = true;
    }

    IEnumerator EMPDuration()
    {
        yield return new WaitForSeconds(enemyDeactivationTime);
        foreach (GameObject enemy in enemies)
        {
            try
            {
                enemy.GetComponent<IEnemy>().SetActiveBehaviourTrue();
            }
            catch { }
        }
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] goen;
        GameObject[] gobo;
        GameObject[] gos;
        goen = GameObject.FindGameObjectsWithTag("Enemy");
        gobo = GameObject.FindGameObjectsWithTag("Boss");
        gos = goen.Concat(gobo).ToArray();
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            try
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance && curDistance > 0.0f && FindPlayerRoom() == go.GetComponent<IEnemy>().GetParent().transform.parent.gameObject)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            catch { }

        }
        return closest;
    }

    public List<GameObject> FindAllEnemiesInRoom()
    {
        GameObject closest = FindClosestEnemy();
        List<GameObject> enes = new List<GameObject>();
        if (closest)
        {
            GameObject[] goen;
            GameObject[] gobo;
            GameObject[] gos;
            goen = GameObject.FindGameObjectsWithTag("Enemy");
            gobo = GameObject.FindGameObjectsWithTag("Boss");
            gos = goen.Concat(gobo).ToArray();
            foreach (GameObject go in gos)
            {
                try
                {
                    if (closest.GetComponent<IEnemy>().GetParent().transform.parent == go.GetComponent<IEnemy>().GetParent().transform.parent)
                    {
                        enes.Add(go);
                    }
                }
                catch { }
            }
        }
        return enes;
    }

    public GameObject FindPlayerRoom()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Room");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }

        }
        return closest;
    }
}
