using UnityEngine;

public interface IBulletShoot
{
    void InstantiateBullet(WeaponSO weaponData, Transform firePoint);
}