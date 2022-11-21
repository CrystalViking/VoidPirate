using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MedkitPickup : PickableItem, IPickable
{
    [SerializeField] ScriptableMedkit medkit;
    void Update()
    {
        InteractOnPickup();
        InteractOnStackup();
    }

    //FindObjectOfType

    public override void InteractOnPickup()
    {
        if (Input.GetKeyDown(itemPickupCode) && isInRange)
        {
            HealPlayer();

            if (destroyOnPickUp)
            {
                Destroy(gameObject);
            }
        }
    }

    public void InteractOnStackup()
    {
        if (Input.GetKeyDown(itemStackUpCode) && isInRange)
        {
            PlayerStackup();

            if (destroyOnPickUp)
            {
                Destroy(gameObject);
            }
        }
    }

    private void PlayerStackup()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<ConsumableInventory>().AddMedkit(medkit);
    }

    private void HealPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Heal(medkit.hp_content);
    }





}
