using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool initializeDataIfNull = false;


    [Header("File Storage Config")]
    [SerializeField] private string fileName;



    private GameData gameData;

    private List<IDataPersistence> dataPersistenceObjects;
    private FileDataHandler fileDataHandler;

    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than one Data Persistence Manager in the scene. Destroying the newest one.");
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);

        this.fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }

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
        this.dataPersistenceObjects = FindAllDataPersistenceObjects();
        //LoadGame();
    }

    

    private void Start()
    {
        
        
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return gameData != null;
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        if(SceneManager.GetActiveScene().name != "TutorialMapMainGameScene" &&
            SceneManager.GetActiveScene().name != "LobbyShipTutorial")
        {
            this.dataPersistenceObjects = FindAllDataPersistenceObjects();

            this.gameData = fileDataHandler.Load();

            if (this.gameData == null && initializeDataIfNull)
            {
                NewGame();
            }

            if (this.gameData == null)
            {
                Debug.Log("No data was found. A New Game needs to be started before data can be loaded.");
                //NewGame();
            }


            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.LoadData(gameData);
            }
        }

        
    }

    //public void LobbyShipSave()
    //{ 
    //    if(SceneManager.GetActiveScene() != null && SceneManager.GetActiveScene().name == "LobbyShipFinal")
    //    {
    //        // Try to save stuff
    //        SaveGame();
    //    }
    
    //}

    public void SaveGame()
    {
        if (SceneManager.GetActiveScene().name != "TutorialMapMainGameScene" &&
            SceneManager.GetActiveScene().name != "LobbyShipTutorial")
        {

            if (this.gameData == null && initializeDataIfNull)
            {
                NewGame();
            }

            if (this.gameData == null)
            {
                Debug.LogWarning("No data was found. A New Game needs to be started before data can be saved.");
                return;
            }

            foreach (IDataPersistence dataPersistenceObj in dataPersistenceObjects)
            {
                dataPersistenceObj.SaveData(gameData);
            }

            //Debug.Log("Saved coin count = " + gameData.coinCount);

            fileDataHandler.Save(gameData);
        }
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
}
