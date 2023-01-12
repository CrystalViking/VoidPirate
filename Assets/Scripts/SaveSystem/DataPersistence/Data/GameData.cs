using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int coinCount;
    public int totalCoinCount;

    public int day;

    public SavableWeapon primaryWeapon;
    public SavableWeapon secondaryWeapon;

    public bool newGame;

    public bool levelFinished;

    public bool ifBossLevel;
    public string currentLevel;



    public int spaceshipProbability;

    public int bulletHellProbability;

    public LobbyTravelState lobbyTravelState;

    public LobbyBossState lobbyBossState;

    public LevelRealm levelRealm;


    public List<SavableBuyOption> buyOptionsPrimary;
    public List<SavableBuyOption> buyOptionsSecondary;


    public GameData()
    {
        //this.coinCount = 0;
    }
}

public class SavableWeapon
{
    public string id;

    public SavableWeapon(string id)
    {
        this.id = id;
    }
}

public class SavableBuyOption
{
    public string id; // nor sure about this one
    public bool purchased;
}

public class SavableSelectOption
{
    public string id;
}



public enum LobbyTravelState
{
    OnDungeonLevel,
    OnSpaceshipLevel,
    ReadyToTravel,
    OnBossLevel,
    EndGame
}

public enum LobbyBossState
{
    bossLocationUnknown,
    bossLocationUnlocked,
    bossLocationSet,
    bossDefeated
}

public enum LevelRealm
{
    EstrellaRealm,
    ReaperRealm,
    AtarosRealm,
}




