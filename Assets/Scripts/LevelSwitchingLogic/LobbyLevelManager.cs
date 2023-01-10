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
    public bool atarosAwailable;
    public bool atarosDefeated;

    public bool levelFinished;

    public bool ifBossLevel;

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
        atarosAwailable = data.atarosAwailable;
        atarosDefeated = data.atarosDefeated;

        ifBossLevel = data.ifBossLevel;

        levelFinished = data.levelFinished;
        currentLevel = data.currentLevel;
    }

    public void SaveData(GameData data)
    {
        data.lobbyTravelState = travelState;
        data.lobbyBossState = bossState;
        data.levelRealm = levelRealm;

        data.estrellaCoordinatesUnlocked = estrellaCoordinatesUnlocked;
        data.estrellaAwailable = estrellaAwailable;
        data.estrellaDefeated = estrellaDefeated;

        data.reaperCoordinatesUnlocked = reaperCoordinatesUnlocked;
        data.reaperAwailable = reaperAwailable;
        data.reaperDefeated = reaperDefeated;

        data.atarosCoordinatesUnlocked = atarosCoordinatesUnlocked;
        data.atarosAwailable = atarosAwailable;
        data.atarosDefeated = atarosDefeated;

        data.ifBossLevel = ifBossLevel;

        data.levelFinished = levelFinished;
        data.currentLevel = currentLevel;

    }

    public void SetLevelOnTerminal()
    {
        if (bossState == LobbyBossState.bossLocationUnknown)
        {
            if (travelState == LobbyTravelState.OnDungeonLevel)
            {
                switch (levelRealm)
                {
                    case (LevelRealm.EstrellaRealm):
                        //set teleport to estrella dungeon
                        break;
                    case (LevelRealm.ReaperRealm):
                        //set teleport to reaper realm
                        break;
                    case (LevelRealm.AtarosRealm):
                        //set teleport to ataros realm
                        break;
                }
            }
            else if (travelState == LobbyTravelState.OnSpaceshipLevel)
            {
                // set teleport point to Spaceship

            }
            else if (travelState == LobbyTravelState.ReadyToTravel)
            {
                // calculate probability of spaceship and set travel point to next level
            }
        }
    }
}
