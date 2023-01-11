using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int coinCount;
    public int totalCoinCount;


    public SavableWeapon primaryWeapon;
    public SavableWeapon secondaryWeapon;

    public bool newGame;

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



    public int spaceshipProbability;

    public int bulletHellProbability;

    public LobbyTravelState lobbyTravelState;

    public LobbyBossState lobbyBossState;

    public LevelRealm levelRealm;


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

}



public enum LobbyTravelState
{
    OnDungeonLevel = 100,
    OnSpaceshipLevel = 200,
    ReadyToTravel = 300,
    OnBossLevel = 400
}

public enum LobbyBossState
{
    bossLocationUnknown = 100,
    bossLocationUnlocked = 200,
    bossLocationSet = 300,
    bossDefeated = 400
}

public enum LevelRealm
{
    EstrellaRealm = 100,
    ReaperRealm = 200,
    AtarosRealm = 300,
}




