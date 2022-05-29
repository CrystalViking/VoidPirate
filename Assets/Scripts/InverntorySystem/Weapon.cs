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
    public GameObject projectilePrefab;

    public float minDamage;
    public float maxDamage;
    public float spread, range, reloadTime, timeBetweenShots;
    public int magazineSize; 
    public float bulletsPerTap;
    public bool allowButtonHold;
    int bulletsLeft, bulletsShot;
    public WeaponType weaponType;
    public WeaponSlot weaponSlot;

    public float projectileVelocity;
    public float weaponScreenShake;
    public float projectileForce;


    public int storedAmmo;

    public float fireRate;
    

    bool shooting, readyToShoot, reloading;


}

