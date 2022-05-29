using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private int currentlyEquippedWeapon = 0;
    [SerializeField]public PlayerInventory inventory;
    private GameObject currentWeaponObject = null;

    [SerializeField] Weapon defaultMeleeWeapon = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1) && currentlyEquippedWeapon != 0)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(0));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentlyEquippedWeapon != 1)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(1));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentlyEquippedWeapon != 2)
        {
            UnequipWeapon();
            EquipWeapon(inventory.GetItem(2));
        }
    }
    
    
    private void InstantiateWeapon(GameObject weaponObject, int weaponSlot)
    {
        Weapon weapon = inventory.GetItem(weaponSlot);
    }

    private void EquipWeapon(Weapon weapon)
    {
        currentlyEquippedWeapon = (int)weapon.weaponSlot;
        //currentWeaponObject = Instantiate(weapon.prefab);
    }

    private void UnequipWeapon()
    {
        Destroy(currentWeaponObject);
    }

}
