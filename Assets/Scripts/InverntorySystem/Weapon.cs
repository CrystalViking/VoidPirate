using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType
{
    Melee,
    Pistol,
    AR,
    Shotgun,
    Special
}

public enum WeaponSlot
{
    Primary, Secondary, Melee
}


[CreateAssetMenu(fileName = "new Weapon", menuName = "Items/Weapon")]
public class Weapon : Item
{
    public GameObject prefab;

    public int damage;
    public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots;
    public int magazineSize, bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    public WeaponType weaponType;
    public WeaponSlot weaponSlot;

    bool shooting, readyToShoot, reloading;


}

