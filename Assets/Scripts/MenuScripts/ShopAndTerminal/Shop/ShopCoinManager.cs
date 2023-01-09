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

    public event EventHandler<ItemPurchasedEventArgs> PurchaseFinished;
    
    void Start()
    {
        totalCoins = 10000;
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
        totalCoins += data.coinCount;
        data.coinCount = 0;
    }

    public void SaveData(GameData data)
    {
        data.totalCoinCount = totalCoins;
    }
}

public class ItemPurchasedEventArgs : EventArgs
{
    public string TitleText { get; set; }
}
