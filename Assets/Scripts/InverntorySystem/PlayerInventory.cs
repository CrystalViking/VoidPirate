using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] private Weapon[] weapons;

    private WeaponShooting shooting;

    private EquipmentManager equipmentManager;

    private void InitVariables()
    {
        weapons = new Weapon[3];
    }


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

        shooting.InitAmmo((int)newItem.weaponSlot, newItem);

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
        if(weapons != null && weapons.Length != 0)
        {
            return weapons[index];
        }
        return null;
    }

    public int GetInventorySize()
    {
        return weapons.Length;
    }

    

    private void GetReferences()
    {
        equipmentManager = GetComponent<EquipmentManager>();
        shooting = GetComponent<WeaponShooting>();
    }

   
}
