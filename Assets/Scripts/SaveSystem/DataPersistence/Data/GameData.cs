using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData 
{
    public int coinCount;
    public int totalCoinCount;

    //public List<SavableWeapon> savableWeapons;

    public SavableWeapon primaryWeapon;
    public SavableWeapon secondaryWeapon;


    public bool estrellaCoordinatesUnlocked;
    public bool estrellaAwailable;

    public bool reaperCoordinatesUnlocked;
    public bool reaperAwailable;

    public bool atarosCoordinatesUnlocked;
    public bool atarosUnlocked;




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
