using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopWeaponManager : MonoBehaviour
{
    //[SerializeField]
    //List<ShopWeaponSO> weaponDataList;

    //[SerializeField]
    //GameObject button;

    //[SerializeField]
    //GameObject contentContainer;

    public ShopWeaponSO[] shopWeaponsSO;
    public GameObject[] shopOptionsGO;
    public ShopItemTemplate[] shopOptions;

    

    void Start()
    {
        LoadPanels();
        ActivatePanels();
    }

    
    void Update()
    {
        
    }

    //void PopulateMenuContainer()
    //{
    //    if(weaponDataList.Count > 0)
    //    {
    //        foreach(ShopWeaponSO weapon in weaponDataList)
    //        {

    //        }
    //    }
    //}


    public void LoadPanels()
    {
        for (int i = 0; i < shopOptions.Length; i++)
        {
            shopOptions[i].shopOptionData = shopWeaponsSO[i];
            
        }
    }

    public void ActivatePanels()
    {
        for(int i = 0; i < shopWeaponsSO.Length; i++)
        {
            shopOptionsGO[i].SetActive(true);
        }
    }

    void InitVariables()
    {
        //weaponDataList = new List<WeaponSO>();



    }








}
