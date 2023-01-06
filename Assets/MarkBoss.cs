using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkBoss : MonoBehaviour
{
    private GameObject boss;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Mark()
    {

        boss = GameObject.FindGameObjectWithTag("Boss");
        if (boss != null)
        {
            TargetIndicator.instance.MarkTarget(boss.transform);
        }
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
    public GameObject FindBoss()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Boss");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            try
            {
                Vector3 diff = go.transform.position - position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < distance && curDistance > 0.0f)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            catch { }

        }
        return closest;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        try
        {
            if (FindPlayerRoom() == FindBoss().GetComponent<IEnemy>().GetParent().transform.parent.gameObject)
            {
                gameObject.SetActive(false);
            }
            else
            {
                Mark();
            }
        }
        catch
        {
            Mark();
        }
        
    }
}
