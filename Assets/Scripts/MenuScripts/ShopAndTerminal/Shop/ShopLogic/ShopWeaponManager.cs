using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeaponManager : MonoBehaviour
{
    [SerializeField]
    List<ShopWeaponSO> weaponDataList;

    [SerializeField]
    GameObject button;

    [SerializeField]
    GameObject contentContainer;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    void PopulateMenuContainer()
    {
        if(weaponDataList.Count > 0)
        {
            foreach(ShopWeaponSO weapon in weaponDataList)
            {

            }
        }
    }


    void InitVariables()
    {
        //weaponDataList = new List<WeaponSO>();



    }








}
