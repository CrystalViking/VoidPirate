using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MedkitPickup : PickableItem, IStackable, IPickable
{
    [SerializeField] ScriptableMedkit medkit;
    void Update()
    {
        InteractOnPickup();
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
        if (Input.GetKeyDown(itemStackupCode) && isInRange)
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
        throw new NotImplementedException();
    }




    private void HealPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Heal(medkit.hp_content);
    }





}
