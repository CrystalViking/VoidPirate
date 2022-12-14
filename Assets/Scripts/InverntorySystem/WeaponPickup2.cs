using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup2 : PickableItem, IPickable
{
    [SerializeField] WeaponSO weapon;


    void Update()
    {
        InteractOnPickup();
    }

    public override void InteractOnPickup()
    {
        if (Input.GetKeyDown(itemPickupCode) && isInRange)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<GunManager>().AddGun(weapon.prefab);

            if (destroyOnPickUp)
            {
                Destroy(gameObject);
            }
        }
    }
}
