using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Ammobox", menuName = "Items/AmmoBox")]
public class ScriptableAmmoBox : Item
{
    public float minAmmo;
    public float maxAmmo;
    public WeaponType ammoType;



}
