using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ShopWeapon", menuName = "Items/ShopWeapon")]
public class ShopWeaponSO : ScriptableObject
{
    public WeaponSO weaponSO;

    public string title;
    public string description;
    public Sprite correspondingImage;

    public int price;


}
