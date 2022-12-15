using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerCurrency : MonoBehaviour
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins(int coinAmount)
    {
        total = total + coinAmount * singleCoinValue;
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
}
