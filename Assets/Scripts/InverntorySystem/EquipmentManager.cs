using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    private int currentlyEquippedWeapon = 0;
    private PlayerInventory playerInventory;
    private GameObject currentWeaponObject = null;

    private HudScript playerHud;

    private int EMprimaryCurrentAmmo;
    private int EMprimaryCurrentAmmoStorage;

    private int EMsecondaryCurrentAmmo;
    private int EMsecondaryCurrentAmmoStorage;

    private WeaponParent weaponParent;

    [SerializeField] Weapon defaultMeleeWeapon = null;

    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
        StartCoroutine(LetOthersCatchUp());
        
        EquipWeapon(defaultMeleeWeapon);
        
    }

    private void Awake()
    {
        weaponParent = GetComponentInChildren<WeaponParent>();
    }

    IEnumerator LetOthersCatchUp()
    {
        yield return new WaitForSeconds(1);
        playerInventory.AddItem(defaultMeleeWeapon);

    }

    // Update is called once per frame
    void Update()
    {
        // Select weapon from inventory
        if(Input.GetKeyDown(KeyCode.Alpha1) && currentlyEquippedWeapon != 0)
        {
            UnequipWeapon();
            EquipWeapon(playerInventory.GetItem(0));

        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && currentlyEquippedWeapon != 1)
        {
            UnequipWeapon();
            EquipWeapon(playerInventory.GetItem(1));
            

        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && currentlyEquippedWeapon != 2)
        {
            UnequipWeapon();
            EquipWeapon(playerInventory.GetItem(2));
            
        }

        // Update UI
        
    }

    private void UpdateUI()
    {
        var currentWeapon = playerInventory.GetItem(currentlyEquippedWeapon);

        playerHud?.UpdateWeaponUI(currentWeapon);

        if(currentWeapon != null)
        {
            if (currentWeapon.weaponSlot == WeaponSlot.Primary)
                playerHud?.UpdateWeaponAmmoInfo(GetPrimaryAmmo(), GetPrimaryStorage());
            if (currentWeapon.weaponSlot == WeaponSlot.Secondary)
                playerHud?.UpdateWeaponAmmoInfo(GetSecondaryAmmo(), GetSecondaryStorage());

        }

    }
    
    
    private void InstantiateWeapon(GameObject weaponObject, int weaponSlot)
    {
        Weapon weapon = playerInventory.GetItem(weaponSlot);
    }

    public Weapon GetCurrentlyUsedWeaponSO()
    {
        Weapon weapon = playerInventory.GetItem(currentlyEquippedWeapon);
        if (weapon != null)
            return weapon;
        return null;
    }

    private void EquipWeapon(Weapon weapon)
    {
        if(weapon != null)
        {
            DisableWeaponParentIfNotMelee((int)weapon.weaponSlot);

            currentlyEquippedWeapon = (int)weapon.weaponSlot;
            UpdateUI();
        }

        
        //Debug.Log(currentlyEquippedWeapon.ToString());
        //currentWeaponObject = Instantiate(weapon.prefab);
    }

    public void EquipPickup(int weaponSlot)
    {
        DisableWeaponParentIfNotMelee(weaponSlot);

        currentlyEquippedWeapon = weaponSlot;
        UpdateUI();
    }

    private void DisableWeaponParentIfNotMelee(int weaponSlot)
    {
        if(weaponSlot != 2)
        {
            transform.Find("WeaponParent").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("WeaponParent").gameObject.SetActive(true);
        }
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
        playerInventory = GetComponent<PlayerInventory>();
        playerHud = GetComponent<HudScript>();
    }

    public void EM_UpdatePrimaryAmmo(int currentAmmoUpdate, int currentStoredAmmoUpdate)
    {
        EMprimaryCurrentAmmo = currentAmmoUpdate;
        EMprimaryCurrentAmmoStorage = currentStoredAmmoUpdate;
    }

    public void EM_UpdateSecondaryAmmo(int currentAmmoUpdate, int currentStoredAmmoUpdate)
    {
        EMsecondaryCurrentAmmo = currentAmmoUpdate;
        EMsecondaryCurrentAmmoStorage = currentStoredAmmoUpdate;
    }


    public int GetPrimaryAmmo()
    {
        return EMprimaryCurrentAmmo;
    }

    public int GetPrimaryStorage()
    {
        return EMprimaryCurrentAmmoStorage;
    }


    public int GetSecondaryAmmo()
    {
        return EMsecondaryCurrentAmmo;
    }

    public int GetSecondaryStorage()
    {
        return EMsecondaryCurrentAmmoStorage;
    }


}
