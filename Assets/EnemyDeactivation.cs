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
    
    public Canvas canvasEMP; 
    public bool isTutorialOrShipLevel = false;

    [Header("Sound effects")]
    public AudioSource audioSource;
    public AudioSource audioSource2;
    void Start()
    {
        canEMP = true;
    }


    void Update()
    {   
        if (Input.GetKeyDown(KeyCode.Space) && canEMP)
        {
            EMP();
        }
    }

    private void EMP()
    {
        if (isTutorialOrShipLevel)
            enemies = FindAllEnemies();
        else
            enemies = FindAllEnemiesInRoom();
        audioSource.Play();
        foreach (GameObject enemy in enemies)
        {
             enemy.GetComponent<IEnemy>().SetActiveBehaviourFalse();
        }
        canEMP = false;
        if(canvasEMP)
            canvasEMP.enabled = false;
        StartCoroutine(EMPCooldown());
        StartCoroutine(EMPDuration());
    }   
    IEnumerator EMPCooldown()
    {
        yield return new WaitForSeconds(cooldownEMP);
        audioSource2.Play();
        canEMP = true;
        if (canvasEMP)
            canvasEMP.enabled = true;
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
        GameObject closest = FindPlayerRoom();
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
                    if (closest.transform == go.GetComponent<IEnemy>().GetParent().transform.parent)
                    {
                        if(go.GetComponent<SpawnerEnemy>())
                        {
                            List<GameObject> list = go.GetComponent<SpawnerEnemy>().GetMinionList();
                            foreach (GameObject g in list)
                            {
                                enes.Add(g);
                            }
                        }
                        enes.Add(go);
                    }
                }
                catch { }
            }
        }
        return enes;
    }

    public List<GameObject> FindAllEnemies()
    {
        List<GameObject> enes = new List<GameObject>();
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

                if (go.GetComponent<SpawnerEnemy>())
                {
                    List<GameObject> list = go.GetComponent<SpawnerEnemy>().GetMinionList();
                    foreach (GameObject g in list)
                    {
                        enes.Add(g);
                    }
                }
                enes.Add(go);
                    
            }
            catch { }
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
