using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyLevelManager : MonoBehaviour, IDataPersistence
{

    [SerializeField]
    private GameObject travelTerminal;

    [SerializeField]
    private GameObject bossCoordinatesMiniGame;


    [SerializeField]
    private GameObject playerWeaponParent;

    [SerializeField]
    private GameObject firstWeapon;

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

    public bool newGame;


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

        

        ifBossLevel = data.ifBossLevel;

        levelFinished = data.levelFinished;
        currentLevel = data.currentLevel;

        newGame = data.newGame;

        if(newGame)
        {
            NewGameStarter();
        }    
        LevelFinshedState();
        SetLevelOnTerminal();
        
    }

    public void SaveData(GameData data)
    {
        data.lobbyTravelState = travelState;
        data.lobbyBossState = bossState;
        data.levelRealm = levelRealm;

        

        

        
        data.ifBossLevel = ifBossLevel;

        data.levelFinished = levelFinished;
        data.currentLevel = currentLevel;

        data.newGame = newGame;

    }


    // Main Logic
    public void SetLevelOnTerminal()
    {
        //test logic
        //bossState = LobbyBossState.bossLocationUnlocked;
        
        //end test logic
        if(travelState != LobbyTravelState.EndGame)
        {
            if (bossState == LobbyBossState.bossLocationUnknown)
            {
                if (travelState != LobbyTravelState.ReadyToTravel)
                {
                    if (travelState == LobbyTravelState.OnDungeonLevel)
                    {
                        switch (levelRealm)
                        {
                            case (LevelRealm.EstrellaRealm):
                                //FindObjectOfType<TerminalLevelLogic>(true)
                                //    .GetComponent<TerminalLevelLogic>()
                                //    .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                //                            ESTRELLA_REALM,
                                //                            IS_EMPTY_LEVEL);
                                travelTerminal
                                    .GetComponent<TerminalLevelLogic>()
                                    .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                                            ESTRELLA_REALM,
                                                            IS_EMPTY_LEVEL);

                                break;
                            case (LevelRealm.ReaperRealm):
                                travelTerminal
                                    .GetComponent<TerminalLevelLogic>()
                                    .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                                            REAPER_REALM,
                                                            IS_EMPTY_LEVEL);
                                break;
                            case (LevelRealm.AtarosRealm):
                                travelTerminal
                                    .GetComponent<TerminalLevelLogic>()
                                    .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                                            ATAROS_REALM,
                                                            IS_EMPTY_LEVEL);
                                break;
                        }
                    }
                    else if (travelState == LobbyTravelState.OnSpaceshipLevel)
                    {
                        travelTerminal
                                    .GetComponent<TerminalLevelLogic>()
                                    .SetTerminalLevelToLoad(SHIP_ROOMS,
                                                            "",
                                                            "");
                    }

                }
                else if (travelState == LobbyTravelState.ReadyToTravel)
                {
                    travelTerminal
                                .GetComponent<TerminalLevelLogic>()
                                .ActivateNextLevelButton();
                }
            }
            else if (bossState == LobbyBossState.bossLocationUnlocked)
            {
                //activate minigame
                bossCoordinatesMiniGame.GetComponent<BossCoordinatesActivator>().SetCoordinatesScanned();
            }
            else if (bossState == LobbyBossState.bossLocationSet)
            {
                //send travel point to boss or activate teleport when arrived
                //bossCoordinatesMiniGame.GetComponent<BossCoordinatesActivator>().SetCoordinatesScanned();

                if (travelState == LobbyTravelState.OnBossLevel)
                {
                    switch (levelRealm)
                    {
                        case (LevelRealm.EstrellaRealm):
                            travelTerminal
                                .GetComponent<TerminalLevelLogic>()
                                .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                                        ESTRELLA_REALM,
                                                        IS_BOSS_LEVEL);

                            break;
                        case (LevelRealm.ReaperRealm):
                            travelTerminal
                                .GetComponent<TerminalLevelLogic>()
                                .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                                        REAPER_REALM,
                                                        IS_BOSS_LEVEL);
                            break;
                        case (LevelRealm.AtarosRealm):
                            travelTerminal
                                .GetComponent<TerminalLevelLogic>()
                                .SetTerminalLevelToLoad(DUNGEON_ROOMS,
                                                        ATAROS_REALM,
                                                        IS_BOSS_LEVEL);
                            break;
                    }
                }
                else
                {
                    //travelState = LobbyTravelState.ReadyToTravel;
                    //DataPersistenceManager.instance.SaveGame();
                    //SetLevelOnTerminal();
                    travelTerminal
                                .GetComponent<TerminalLevelLogic>()
                                .ActivateNextLevelButton();

                    //DataPersistenceManager.instance.SaveGame();
                    //DataPersistenceManager.instance.LoadGame();
                }


            }
            else if (bossState == LobbyBossState.bossDefeated)
            {
                //bossState = LobbyBossState.bossLocationUnknown;
                //travelState = LobbyTravelState.ReadyToTravel;

                travelTerminal
                                .GetComponent<TerminalLevelLogic>()
                                .ActivateNextLevelButton();

                switch (levelRealm)
                {
                    case (LevelRealm.EstrellaRealm):
                        levelRealm = LevelRealm.ReaperRealm;
                        break;
                    case (LevelRealm.ReaperRealm):
                        levelRealm = LevelRealm.AtarosRealm;
                        break;
                    case (LevelRealm.AtarosRealm):
                        travelState = LobbyTravelState.EndGame;
                        travelTerminal
                            .GetComponent<TerminalLevelLogic>()
                            .ActivateAllBosses();
                        break;
                }

                DataPersistenceManager.instance.SaveGame();
                //DataPersistenceManager.instance.LoadGame();
            }
        }
        else
        {
            travelTerminal
                .GetComponent<TerminalLevelLogic>()
                .ActivateAllBosses();
        }

        

    }


    public void SetBossLocation(LobbyBossState bossState)
    {
        this.bossState = bossState;
        DataPersistenceManager.instance.SaveGame();
        SetLevelOnTerminal();
    }

   
    private void NewGameStarter()
    {
        newGame = false;
        DataPersistenceManager.instance.SaveGame();
        playerWeaponParent.GetComponent<GunManager>().AddGun(firstWeapon);

    }

    

    

    
    private void LevelFinshedState()
    {
        if(levelFinished)
        {
            travelState = LobbyTravelState.ReadyToTravel;
            levelFinished = false;
            DataPersistenceManager.instance.SaveGame();
        }
    }

}
