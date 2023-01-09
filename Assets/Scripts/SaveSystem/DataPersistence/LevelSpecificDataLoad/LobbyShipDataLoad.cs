using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyShipDataLoad : MonoBehaviour
{
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //if(FindObjectOfType<>)
        if(FindObjectOfType<EventManager>() != null && !FindObjectOfType<EventManager>().EventStatus())
        {
            DataPersistenceManager.instance.LoadGame();
        }
    }
}
