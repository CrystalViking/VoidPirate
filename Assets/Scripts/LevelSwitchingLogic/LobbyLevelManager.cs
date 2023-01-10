using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLevelManager : MonoBehaviour, IDataPersistence
{

    private LobbyTravelState travelState;
    private LobbyBossState bossState;
    public LevelRealm levelRealm;

    public bool estrellaCoordinatesUnlocked;
    public bool estrellaAwailable;
    public bool estrellaDefeated;

    public bool reaperCoordinatesUnlocked;
    public bool reaperAwailable;
    public bool reaperDefeated;

    public bool atarosCoordinatesUnlocked;
    public bool atarosUnlocked;
    public bool atarosDefeated;

    public bool levelFinished;

    public string currentLevel;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        travelState = data.lobbyTravelState;
        bossState = data.lobbyBossState;
        levelRealm = data.levelRealm;

        estrellaCoordinatesUnlocked = data.estrellaCoordinatesUnlocked;
        estrellaAwailable = data.estrellaAwailable;
        estrellaDefeated = data.estrellaDefeated;   

        reaperCoordinatesUnlocked = data.reaperCoordinatesUnlocked;
        reaperAwailable = data.reaperAwailable;
        reaperDefeated = data.reaperDefeated;

        atarosCoordinatesUnlocked = data.atarosCoordinatesUnlocked;
        atarosUnlocked = data.atarosDefeated;
        atarosDefeated = data.atarosDefeated;

        levelFinished = data.levelFinished;
        currentLevel = data.currentLevel;
    }

    public void SaveData(GameData data)
    {
        throw new System.NotImplementedException();
    }
}
