using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrency : MonoBehaviour
{
    int total;
    CharacterStat statSingleCoinValue;
    int singleCoinValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCoins(int coinAmount)
    {
        total = coinAmount * singleCoinValue;
    }

    public void SpendCoins(int amount)
    {

        if(total >= amount)
        total -= amount;
    }

    public void SetSingleCoinValue(int value)
    {
        singleCoinValue = value;
    }
}
