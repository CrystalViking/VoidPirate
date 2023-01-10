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

    public bool reaperCoordinatesUnlocked;
    public bool reaperAwailable;

    public bool atarosCoordinatesUnlocked;
    public bool atarosUnlocked;

    public bool ifBossLevel;
    public string currentLevel;


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
    OnLevel,
    ReadyToTravel
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
    AtarosRealm
}

