using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSceneTimer : MonoBehaviour
{
    SceneLoader loader;
    void Start()
    {
        loader = GetComponent<SceneLoader>();
        StartCoroutine(Transit());
    }


    IEnumerator Transit()
    {
        yield return new WaitForSeconds(17f);
        StartCoroutine(loader.LoadScene("LobbyShip"));
    }

    
}
