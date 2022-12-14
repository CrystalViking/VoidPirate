using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private GameObject[] guns;

    [SerializeField]
    private int selectedWeapon ;

    private GameObject currentWeapon;

    private HudScript hudScript;


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
        if(currentWeapon.GetComponent<IWeaponData>().GetWeaponSlot() == WeaponSlot.Melee)
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

    public void AddAmmoToCurrentWeaponType(WeaponType weaponType, int currentStoredAmmoAdded)
    {
        for(int i = 0; i < transform.childCount; i++)
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
        

        
    }

    public void AddGun(int slot, GameObject equipabbleWeapon)
    {
        guns[slot] = equipabbleWeapon;

        equipabbleWeapon = Instantiate(equipabbleWeapon, transform, false) as GameObject;
        //equipabbleWeapon.transform.parent = transform;

        //GameObject go = Instantiate(A, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        //go.transform.parent = GameObject.Find("Stage Scroll").transform;
    }
}
