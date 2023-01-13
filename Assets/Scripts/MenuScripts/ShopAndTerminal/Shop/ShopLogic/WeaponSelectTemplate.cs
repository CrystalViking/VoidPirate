using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class WeaponSelectTemplate : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    public WeaponSO weaponData;


    public TMP_Text titleText;

    

    public void OnPointerClick(PointerEventData eventData)
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if(gameObject.GetComponent<WeaponSelectTemplate>().weaponData.ItemName == weaponData.ItemName)
        {
            player.GetComponentInChildren<GunManager>()
                .AddGun(gameObject.GetComponent<WeaponSelectTemplate>().weaponData.prefab);


            if (weaponData.weaponSlot == WeaponSlot.Primary)
            {
                GetComponentInParent<GeneralShopManager>()
                    .UpdateSelectWeaponPrimary(weaponData.ItemName.ToUpper());
            }
            else if(weaponData.weaponSlot == WeaponSlot.Secondary)
            {
                GetComponentInParent<GeneralShopManager>()
                    .UpdateSelectWeaponSecondary(weaponData.ItemName.ToUpper());
            }
                

        }

        
    }

    

    void Start()
    {
        titleText.text = weaponData.ItemName.ToUpper();
        gameObject.SetActive(true);
    }

    
    void Update()
    {
        
    }
}
