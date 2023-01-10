using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameSetup : MonoBehaviour, IDataPersistence
{

    //OnLevelLoaded check if save exists, otherwise set data.newGame = true;

    bool newGame = false;

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
        // Loading will be called manually

        //Debug.Log("OnSceneLoaded Called");
        //this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        DataPersistenceManager.instance.LoadGame();
    }

    public void SetNewGame()
    {
        newGame = true;
        DataPersistenceManager.instance.SaveGame();
    }

    public void LoadData(GameData data)
    {
        if(DataPersistenceManager.instance.HasGameData())
        {
            newGame = data.newGame;
        }
    }

    public void SaveData(GameData data)
    {
        data.newGame = newGame;
    }
}
