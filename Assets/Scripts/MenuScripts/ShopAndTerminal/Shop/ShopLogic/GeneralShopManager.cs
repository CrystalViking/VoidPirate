using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralShopManager : MonoBehaviour
{
    [SerializeField]
    public GameObject selectWeaponListHolder;

    [SerializeField]
    public GameObject selectWeaponDescriptionContainer;


    public void AddWeaponToWeaponListHolder(WeaponSO weaponSO)
    {
        selectWeaponListHolder.GetComponent<SelectWeaponManager>().AddWeaponToList(weaponSO);
    }

    public void UpdateSelectWeaponPrimary(string primary)
    {
        selectWeaponDescriptionContainer.GetComponent<SWDescriptionContainerManager>().SetPrimaryInfoString(primary);
    }
    public void UpdateSelectWeaponSecondary(string secondary)
    {
        selectWeaponDescriptionContainer.GetComponent<SWDescriptionContainerManager>().SetSecondaryInfoString(secondary);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
