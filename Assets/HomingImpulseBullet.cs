using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HomingImpulseBullet : MonoBehaviour
{
    private GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        enemy = FindClosestEnemy();
        if(enemy)
            StartCoroutine(TeleportCoroutine(enemy.transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        enemy = FindClosestEnemy();
        if (enemy)
            StartCoroutine(TeleportCoroutine(enemy.transform.position));
    }

    IEnumerator TeleportCoroutine(Vector2 _target)
    {
        Vector2 current = transform.position;
        Vector2 target = _target;
        float step = 10f * Time.deltaTime;
        while (Vector2.Distance(current, target) > 0.0)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, step);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(target - new Vector2(transform.position.x, transform.position.y)));
            current = transform.position;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

    private GameObject FindClosestEnemy()
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
                if (curDistance < distance && curDistance > 0.0f && curDistance < 20.0f)
                {
                    closest = go;
                    distance = curDistance;
                }
            }
            catch { }

        }
        return closest;
    }
}
