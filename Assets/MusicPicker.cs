using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPicker : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioSource[] bossAudioSources;
    private GameObject player;
    private int i;
    void Start()
    {
        i = Random.Range(0, audioSources.Length);
        Debug.Log(i);
        audioSources[i].Play();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = player.transform.position;
        try
        {
            if (FindPlayerRoom() == FindBoss().GetComponent<IEnemy>().GetParent().transform.parent.gameObject)
            {
                audioSources[i].Stop();
                if (!bossAudioSources[0].isPlaying)
                    bossAudioSources[0].Play();
            }
            else
            {
                bossAudioSources[0].Stop();
                if (!audioSources[i].isPlaying)
                    audioSources[i].Play();
            }
        }
        catch 
        {
            bossAudioSources[0].Stop();
            if (!audioSources[i].isPlaying)
                audioSources[i].Play();
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
