using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private int index;
    [SerializeField]
    private SceneInfo sceneInfo;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            sceneInfo.isEventOn = false;
            StartCoroutine(SceneLoader.instance.LoadScene("BulletHellScene"));
        }
    }
}
