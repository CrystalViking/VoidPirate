using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxPickup : PickableItem, IPickable
{
    [SerializeField] ScriptableAmmoBox ammoBox;

    //private WeaponShooting weaponInterface;
    private GunManager weaponInterface;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        InteractOnPickup();
    }

    private void ReplenishAmmo()
    {
        weaponInterface.AddAmmoToCurrentWeaponType(ammoBox.ammoType, (int)Random.Range(ammoBox.minAmmo, ammoBox.maxAmmo));
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
        if(Input.GetKeyDown(itemPickupCode) && isInRange)
        {          
            ReplenishAmmo();

            if (destroyOnPickUp)
            {
                Destroy(gameObject); 
            }
        }
    }
}
