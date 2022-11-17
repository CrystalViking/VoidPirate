using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : PickableItem, IPickable
{
    [SerializeField] Weapon weapon;
    
   
    void Update()
    {
        InteractOnPickup();
    }

    public override void InteractOnPickup()
    {
        if (Input.GetKeyDown(itemPickupCode) && isInRange)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>().AddItem(weapon);

            if (destroyOnPickUp)
            {
                Destroy(gameObject);
            }
        }
    }


}
