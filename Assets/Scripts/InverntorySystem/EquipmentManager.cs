using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private int currentlyEquippedWeapon = 0;
    private PlayerInventory inventory;
    private GameObject currentWeaponObject = null;

    private HudScript hud;

    [SerializeField] Weapon defaultMeleeWeapon = null;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        inventory.AddItem(defaultMeleeWeapon);
        EquipWeapon(defaultMeleeWeapon);
        
    }

    // Update is called once per frame
    void Update()
    {
        // Select weapon from inventory
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

        // Update UI
        
    }

    private void UpdateUI()
    {
        hud.UpdateWeaponUI(inventory.GetItem(currentlyEquippedWeapon));
    }
    
    
    private void InstantiateWeapon(GameObject weaponObject, int weaponSlot)
    {
        Weapon weapon = inventory.GetItem(weaponSlot);
    }

    private void EquipWeapon(Weapon weapon)
    {
        currentlyEquippedWeapon = (int)weapon.weaponSlot;
        UpdateUI();
        //Debug.Log(currentlyEquippedWeapon.ToString());
        //currentWeaponObject = Instantiate(weapon.prefab);
    }

    public void EquipPickup(int weaponSlot)
    {
        currentlyEquippedWeapon = weaponSlot;
        UpdateUI();
    }

    private void UnequipWeapon()
    {
        Destroy(currentWeaponObject);
    }

    public int CurrentlyEquippedWeapon()
    {
        return currentlyEquippedWeapon;
    }

    private void GetReferences()
    {
        inventory = GetComponent<PlayerInventory>();
        hud = GetComponent<HudScript>();
    }

}
