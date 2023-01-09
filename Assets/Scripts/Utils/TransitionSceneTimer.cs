using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionSceneTimer : MonoBehaviour
{
    [SerializeField]
    private SceneInfo sceneInfo;
    SceneLoader loader;
    void Start()
    {
        sceneInfo.isEventOn = false;
        loader = GetComponent<SceneLoader>();
        StartCoroutine(Transit());
    }


    IEnumerator Transit()
    {
        yield return new WaitForSeconds(10f);

        GetComponentInChildren<AsyncLoader>().LoadLevelButton("LobbyShipFinal");
        //StartCoroutine(loader.LoadScene("LobbyShipFinal"));
    }

    
}
