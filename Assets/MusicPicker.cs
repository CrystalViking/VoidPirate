using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPicker : MonoBehaviour
{
    public AudioSource[] audioSources;
    void Start()
    {
        audioSources[0].Play();
    }

    void Update()
    {
        try
        {
            if (FindPlayerRoom() == FindBoss().GetComponent<IEnemy>().GetParent().transform.parent.gameObject)
            {
                audioSources[0].Stop();
                if (!audioSources[1].isPlaying)
                    audioSources[1].Play();
            }
            else
            {
                audioSources[1].Stop();
                if (!audioSources[0].isPlaying)
                    audioSources[0].Play();
            }
        }
        catch 
        {
            audioSources[1].Stop();
            if (!audioSources[0].isPlaying)
                audioSources[0].Play();
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

}
