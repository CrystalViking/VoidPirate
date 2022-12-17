using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxPickup2 : PickableItem, IPickable
{
    [SerializeField] ScriptableAmmoBox2 ammoBox;

    private GunManager weaponInterface;
    private GameObject player;


    
    void Start()
    {
        GetReferences();
    }

    
    void Update()
    {
        InteractOnPickup();
    }

    private void ReplenishAmmo()
    {
        weaponInterface.AddAmmoToCurrentWeaponSlot(ammoBox.weaponSlot, ammoBox.ammo);
    }


    private void GetReferences()
    {
        player = GameObject.Find("Player");
        //weaponInterface = player.GetComponent<WeaponShooting>();
        weaponInterface = player.GetComponentInChildren<GunManager>();
        isInRange = false;
    }

    public override void InteractOnPickup()
    {
        if (Input.GetKeyDown(itemPickupCode) && isInRange)
        {
            ReplenishAmmo();

            if (destroyOnPickUp)
            {
                Destroy(gameObject);
            }
        }
    }

}
