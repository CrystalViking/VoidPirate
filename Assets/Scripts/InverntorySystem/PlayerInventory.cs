using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Item[] weapons;

    private void Start()
    {
        InitVariables();
    }

    public void AddItem(Item newItem)
    {

    }

    private void InitVariables()
    {
        weapons = new Item[3];
    }
}
