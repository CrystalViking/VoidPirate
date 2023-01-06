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
        yield return new WaitForSeconds(10f);
        StartCoroutine(loader.LoadScene("LobbyShipFinal"));
    }

    
}
