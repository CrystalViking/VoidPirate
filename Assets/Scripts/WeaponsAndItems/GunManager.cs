using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour, IDataPersistence
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] guns;

    [SerializeField]
    private int selectedWeapon ;

    private GameObject currentWeapon;

    private HudScript hudScript;

    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip itemPickup;


    void Start()
    {
        //guns = new GameObject[2];
        hudScript = GetComponentInParent<HudScript>();
        SelectGun();
    }

   

    // Update is called once per frame
    void Update()
    {
        TakeInput();

        UpdateHud();
         
    }

    void UpdateHud()
    {
        if (transform.childCount > 0)
        {
            if (currentWeapon.GetComponent<IWeaponData>().GetWeaponSlot() == WeaponSlot.Melee)
            {
                hudScript?.UpdateWeaponUI(currentWeapon.GetComponent<IWeaponData>().GetWeaponData(),
                                     0,
                                     0);
            }
            else
            {
                hudScript?.UpdateWeaponUI(currentWeapon.GetComponent<IWeaponData>().GetWeaponData(),
                                     currentWeapon.GetComponent<GunShooting>().GetCurrentMagAmmo(),
                                     currentWeapon.GetComponent<GunShooting>().GetCurrentStoredAmmo());
            }
        }
    }


    void TakeInput()
    {
        int previousSelectedWeapon = selectedWeapon;

        if(transform.childCount > 0)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (selectedWeapon >= transform.childCount - 1)
                    selectedWeapon = 0;
                else
                    selectedWeapon++;
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if (selectedWeapon <= 0f)
                    selectedWeapon = transform.childCount - 1;
                else
                    selectedWeapon--;
            }
        }
        

        if(previousSelectedWeapon != selectedWeapon)
        {
            SelectGun();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1) && transform.childCount >= 1)
        {
            SelectWeaponSlot(WeaponSlot.Primary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            SelectWeaponSlot(WeaponSlot.Secondary);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            SelectWeaponSlot(WeaponSlot.Melee);
        }

        //if (previousSelectedWeapon != selectedWeapon)
        //{
        //    SelectGun();
        //}

    }

    public void AddAmmoToCurrentWeaponSlot(WeaponSlot weaponSlot, int ammo)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i)?.GetComponent<IWeaponData>().GetWeaponSlot() == weaponSlot)
            {
                transform.GetChild(i)?.GetComponent<GunShooting>().AddAmmo(ammo);
            }
        }
    }

    public void AddAmmoToCurrentWeaponType(WeaponType weaponType, int currentStoredAmmoAdded)
    {
        audioSource.clip = itemPickup;
        audioSource.volume = 0.6f;
        audioSource.Play();
        for (int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i)?.GetComponent<IWeaponData>().GetWeaponType() == weaponType)
            {
                transform.GetChild(i)?.GetComponent<GunShooting>().AddAmmo(currentStoredAmmoAdded);
            }
        }
    }


    public void EquipGun(int slot)
    {
        
    }

    public void SelectGun()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                currentWeapon = weapon.gameObject;
            }               
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }

    public void SelectWeaponSlot(WeaponSlot weaponSlot)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<IWeaponData>().GetWeaponSlot() == weaponSlot)
            {
                transform.GetChild(i).gameObject.SetActive(true);
                currentWeapon = transform.GetChild(i).gameObject;
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }

    public string GetWeaponNamePrimary()
    {
        if(transform.childCount > 0)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                if(transform.GetChild(i).GetComponent<IWeaponData>().GetWeaponSlot() == WeaponSlot.Primary)
                {
                    if(transform.GetChild(i).gameObject.activeSelf == true)
                    {
                        return transform.GetChild(i).gameObject.GetComponent<IWeaponData>().GetWeaponData().ItemName.ToUpper();
                    }
                    else
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                        string info = transform.GetChild(i).gameObject.GetComponent<IWeaponData>().GetWeaponData().ItemName.ToUpper();
                        transform.GetChild(i).gameObject.SetActive(false);
                        return info;
                    }
                        
                }               
            }
        }
        else
        {
            return "***";
        }
        return "***";
    }

    public string GetWeaponNameSecondary()
    {
        if (transform.childCount > 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<IWeaponData>().GetWeaponSlot() == WeaponSlot.Secondary)
                {
                    if (transform.GetChild(i).gameObject.activeSelf == true)
                    {
                        return transform.GetChild(i).gameObject.GetComponent<IWeaponData>().GetWeaponData().ItemName.ToUpper();
                    }
                    else
                    {
                        transform.GetChild(i).gameObject.SetActive(true);
                        string info = transform.GetChild(i).gameObject.GetComponent<IWeaponData>().GetWeaponData().ItemName.ToUpper();
                        transform.GetChild(i).gameObject.SetActive(false);
                        return info;
                    }
                }
            }
        }
        else
        {
            return "***";
        }
        return "***";
    }

    public void RemoveGun(int index)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(i == index)
            {
                Destroy(transform.GetChild(i).gameObject);
                break;
            }
        }
    }

    public void AddGun(GameObject equipabbleWeapon)
    {
        if(transform.childCount > 0)
        {
            int i = 0;
            bool slotFound = false;
            //GameObject weaponToReplace;
            foreach (Transform weapon in transform)
            {
                if(weapon.gameObject.GetComponent<IWeaponData>().GetWeaponSlot() 
                    == equipabbleWeapon.GetComponent<IWeaponData>().GetWeaponSlot())
                {
                    RemoveGun(i);
                    equipabbleWeapon = Instantiate(equipabbleWeapon, transform, false) as GameObject;
                    equipabbleWeapon.transform.parent = transform;
                    selectedWeapon = i;
                    slotFound = true;
                    //SelectGun(equipabbleWeapon.);
                    SelectWeaponSlot(equipabbleWeapon.gameObject.GetComponent<IWeaponData>().GetWeaponSlot());
                    break;
                }
                i++;
            }
            if(!slotFound)
            {
                equipabbleWeapon = Instantiate(equipabbleWeapon, transform, false) as GameObject;
                equipabbleWeapon.transform.parent = transform;
                selectedWeapon = transform.childCount - 1;
                SelectGun();
            }
        }
        else
        {
            equipabbleWeapon = Instantiate(equipabbleWeapon, transform, false) as GameObject;
            equipabbleWeapon.transform.parent = transform;

            selectedWeapon = transform.childCount - 1;
            SelectGun();
        }

        //DataPersistenceManager.instance.SaveGame();
        
    }

    public void AddGun(int slot, GameObject equipabbleWeapon)
    {
        guns[slot] = equipabbleWeapon;

        equipabbleWeapon = Instantiate(equipabbleWeapon, transform, false) as GameObject;
        //equipabbleWeapon.transform.parent = transform;

        //GameObject go = Instantiate(A, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //go.transform.parent = GameObject.Find("Stage Scroll").transform;
    }

    public void LoadData(GameData data)
    {
        
        
        //check the scene here on in DataPersistenceManager
        if (DataPersistenceManager.instance.HasGameData())
        {
            Reinstantiate(data);
        }
        
        
    }

    public void SaveData(GameData data)
    {
        if (FindObjectOfType<EventManager>() != null && !FindObjectOfType<EventManager>().EventStatus())
        {
            //check the scene here on in DataPersistenceManager
            if (DataPersistenceManager.instance.HasGameData())
            {
                //data.primaryWeapon = new SavableWeapon()

                foreach(Transform weapon in transform)
                {
                    if(weapon?.gameObject.GetComponent<IWeaponData>().GetWeaponSlot() == WeaponSlot.Primary)
                    {
                        data.primaryWeapon = new SavableWeapon(weapon.gameObject.GetComponent<IWeaponData>().GetWeaponData().ItemName);
                    }
                    if (weapon?.gameObject.GetComponent<IWeaponData>().GetWeaponSlot() == WeaponSlot.Secondary)
                    {
                        data.secondaryWeapon = new SavableWeapon(weapon.gameObject.GetComponent<IWeaponData>().GetWeaponData().ItemName);
                    }
                }

            }
        }
    }

    public void Reinstantiate(GameData data)
    {
        var weaponIds = GetComponent<WeaponIdentification>().weaponIds;

        if (data.primaryWeapon != null)
        {
            foreach (var weaponId in weaponIds)
            {
                if(data.primaryWeapon.id == weaponId.prefab.GetComponent<IWeaponData>().GetWeaponData().ItemName)
                {
                    AddGun(weaponId.prefab);
                }
            }
        }
        if(data.secondaryWeapon != null)
        {
            foreach (var weaponId in weaponIds)
            {
                if (data.secondaryWeapon.id == weaponId.prefab.GetComponent<IWeaponData>().GetWeaponData().ItemName)
                {
                    AddGun(weaponId.prefab);
                }
            }
        }
        
    }
}
