using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;


public class ShopCoinManager : MonoBehaviour, IDataPersistence
{
    
    [SerializeField]
    public int totalCoins;

    public UnityEvent<string> OnCoinAmountChange;

    bool coinsAdded = false;

    bool dataLoaded = false;

    public event EventHandler<ItemPurchasedEventArgs> PurchaseFinished;
    
    void Start()
    {
        //totalCoins = 10000;
        OnCoinAmountChange?.Invoke(totalCoins.ToString());
    }



    
    void Update()
    {
        
    }

    public void AddCoins(int coins)
    {
        totalCoins =+ coins;
        OnCoinAmountChange?.Invoke(totalCoins.ToString());
    }

    public void SubtractCoins(int coins)
    {
        totalCoins -= coins;
        OnCoinAmountChange?.Invoke(totalCoins.ToString());
    }

    public int GetTotalCoins()
    {
        return totalCoins;
    }


    public void Purchase(int coins, string title)
    {
        if (coins <= totalCoins)
        {
            SubtractCoins(coins);
            //PurchaseFinished?.Invoke(this, EventArgs.Empty);
            ItemPurchasedEventArgs args = new ItemPurchasedEventArgs();
            args.TitleText = title;
            PurchaseFinished?.Invoke(this, args);
        }
    }

    private void OnItemPurchased(ItemPurchasedEventArgs e)
    {
        EventHandler<ItemPurchasedEventArgs> handler = PurchaseFinished;
        if(handler != null)
        {
            handler(this, e);
        }
    }

    public void LoadData(GameData data)
    {

        totalCoins = data.totalCoinCount;
        if(!coinsAdded)
        {
            totalCoins += data.coinCount;
            coinsAdded = true;
            dataLoaded = true;
            DataPersistenceManager.instance.SaveGame();
        }
        dataLoaded = true;    
        
        OnCoinAmountChange?.Invoke(totalCoins.ToString());
    }

    public void SaveData(GameData data)
    {
        if(dataLoaded)
        {
            data.totalCoinCount = totalCoins;
            if (coinsAdded)
                data.coinCount = 0;
        }
        
        //data.coinCount = 0;
    }
}

public class ItemPurchasedEventArgs : EventArgs
{
    public string TitleText { get; set; }
}
