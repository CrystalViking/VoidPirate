using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MedkitPickup : PickableItem, IPickable
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


    private void HealPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Heal(medkit.hp_content);
    }





}
