using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item: ScriptableObject
{
    public string ItemName;
    public string ItemDescription;
    public Sprite Icon;

    public virtual void Use()
    {
        Debug.Log(ItemName + " was used.");
    }
}
