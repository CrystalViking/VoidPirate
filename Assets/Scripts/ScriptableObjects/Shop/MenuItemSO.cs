using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "new MenuOption", menuName = "Items/MenuOption")]
public class MenuItemSO : ScriptableObject
{
    public string description;

    public Sprite correspondingImage;
    
}
