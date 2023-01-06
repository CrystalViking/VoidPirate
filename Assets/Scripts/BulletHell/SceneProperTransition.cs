using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneProperTransition : MonoBehaviour
{
    [SerializeField] string location_name;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(SceneLoader.instance.LoadScene(location_name));
        }
    }
}
