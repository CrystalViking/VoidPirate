using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLevelManager : MonoBehaviour, IDataPersistence
{

    [SerializeField]
    private GameObject travelTerminal;

    private static string ESTRELLA_REALM = "Estrella";
    private static string REAPER_REALM = "Reaper";
    private static string ATAROS_REALM = "Ataros";

    private static string IS_BOSS_LEVEL = "Yes";
    private static string IS_EMPTY_LEVEL = "No";

    private static string DUNGEON_ROOMS = "DungeonMain";
    private static string SHIP_ROOMS = "MainGameScene";

    public LobbyTravelState travelState;
    public LobbyBossState bossState;
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

        SetLevelOnTerminal();
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
            if(travelState != LobbyTravelState.ReadyToTravel)
            {
                if(travelState == LobbyTravelState.OnDungeonLevel)
                {
                    switch (levelRealm)
                    {
                        case (LevelRealm.EstrellaRealm):
                            FindObjectOfType<TerminalLevelLogic>(true)
                                .GetComponent<TerminalLevelLogic>()
                                .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                                        ESTRELLA_REALM,
                                                        IS_EMPTY_LEVEL);

                            break;
                        case (LevelRealm.ReaperRealm):
                            FindObjectOfType<TerminalLevelLogic>(true)
                                .GetComponent<TerminalLevelLogic>()
                                .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                                        REAPER_REALM,
                                                        IS_EMPTY_LEVEL);
                            break;
                        case (LevelRealm.AtarosRealm):
                            FindObjectOfType<TerminalLevelLogic>(true)
                                .GetComponent<TerminalLevelLogic>()
                                .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                                        ATAROS_REALM,
                                                        IS_EMPTY_LEVEL);
                            break;
                    }                   
                }
                else if(travelState == LobbyTravelState.OnSpaceshipLevel)
                {
                    FindObjectOfType<TerminalLevelLogic>(true)
                                .GetComponent<TerminalLevelLogic>()
                                .SetTerminalLevelToLoad(SHIP_ROOMS,
                                                        "",
                                                        "");
                }
            }
        }
        else if(bossState == LobbyBossState.bossLocationUnlocked)
        {
            //activate minigame
        }
        else if (bossState == LobbyBossState.bossLocationSet)
        {
            //deactivate minigame
        }
        else if (bossState == LobbyBossState.bossDefeated)
        {

        }

    }

    public void LevelFinishedCheck()
    {
        if(levelFinished)
        {
            travelState = LobbyTravelState.ReadyToTravel;
        }
    }


    public void TravelStateLogic()
    {
        if (travelState == LobbyTravelState.OnDungeonLevel)
        {
            switch (levelRealm)
            {
                case (LevelRealm.EstrellaRealm):
                    //set teleport to estrella realm (no boss)
                    break;
                case (LevelRealm.ReaperRealm):
                    //set teleport to reaper realm (no boss)
                    break;
                case (LevelRealm.AtarosRealm):
                    //set teleport to ataros realm (no boss)
                    break;
            }
        }
        //else if (travelState == LobbyTravelState.OnBossLevel)
        //{
        //    switch (levelRealm)
        //    {
        //        case (LevelRealm.EstrellaRealm):
        //            //set teleport to estrella realm (BOSS)
        //            break;
        //        case (LevelRealm.ReaperRealm):
        //            //set teleport to reaper realm (BOSS)
        //            break;
        //        case (LevelRealm.AtarosRealm):
        //            //set teleport to ataros realm (BOSS)
        //            break;
        //    }
        //}
        else if (travelState == LobbyTravelState.OnSpaceshipLevel)
        {
            // set teleport point to Spaceship

        }
        else if (travelState == LobbyTravelState.ReadyToTravel)
        {
            // calculate probability of spaceship and set travel point to next level
        }
       
    }

    public void BossCoordinatesScreen()
    {
        if(bossState == LobbyBossState.bossLocationUnlocked)
        {
            //activate minigame
        }
        if(bossState == LobbyBossState.bossLocationSet)
        {
            //deactivate minigame
        }
    }


    public void SetLobbyTravelState(LobbyTravelState lobbyTravelState)
    {
        travelState = lobbyTravelState;
    }

    public void SetLobbyBossState(LobbyBossState lobbyBossState)
    {
        bossState = lobbyBossState;
    }

    public void SetLevelRealm(LevelRealm levelRealm)
    {
        this.levelRealm = levelRealm;
    }

}
