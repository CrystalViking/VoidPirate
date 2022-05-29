using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    private HudScript hud;

    private EquipmentManager equipmentManager;


    private void Start()
    {
        GetReferences();
        InitVariables();
    }

    private void Update()
    {
        
    }

    public void AddItem(Weapon newItem)
    {
        if(weapons[(int)newItem.weaponSlot] != null)
        {
            RemoveItem((int)newItem.weaponSlot);
        }
        weapons[(int)newItem.weaponSlot] = newItem;

        // Update weaponUI
        //hud.UpdateWeaponUI(newItem);
        // Update EquipmentManager
        equipmentManager.EquipPickup((int)newItem.weaponSlot);
    }

    public void RemoveItem(int index)
    {
        weapons[index] = null;
    }

    public Weapon GetItem(int index)
    {
        return weapons[index];
    }

    private void InitVariables()
    {
        weapons = new Weapon[3];
    }

    private void GetReferences()
    {
        hud = GetComponent<HudScript>();
        equipmentManager = GetComponent<EquipmentManager>();
    }

   
}
