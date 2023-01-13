using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerCurrency : MonoBehaviour, IDataPersistence
{
    public int total;
    CharacterStat statSingleCoinValue;
    int singleCoinValue = 1;

    [SerializeField]
    private UnityEvent<string> onPlayerCurrencyChange;

    // Start is called before the first frame update
    void Start()
    {
        onPlayerCurrencyChange?.Invoke(total.ToString());
        //total = PlayerPrefs.GetInt("coinAmount", 0);
        total = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins(int coinAmount)
    {
        total = total + coinAmount * singleCoinValue;
        //PlayerPrefs.SetInt("coinAmount", total);
        onPlayerCurrencyChange?.Invoke(total.ToString());
    }

    public void SpendCoins(int amount)
    {

        if(total >= amount)
        total -= amount;
        onPlayerCurrencyChange?.Invoke(total.ToString());
    }

    public void SetSingleCoinValue(int value)
    {
        singleCoinValue = value;
    }

    public void LoadData(GameData data)
    {
        //this.total = data.coinCount;
    }

    public void SaveData(GameData data)
    {
        
        if (SceneManager.GetActiveScene().name != "LobbyShipFinal" && SceneManager.GetActiveScene().name != "LobbyShipEvent")
        {
            data.coinCount = total;
        }
        

    }
}
