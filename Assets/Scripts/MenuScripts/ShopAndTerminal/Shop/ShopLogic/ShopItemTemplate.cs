using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopItemTemplate : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IDataPersistence
{
    [SerializeField]
    public ShopWeaponSO shopOptionData;


    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;

    [SerializeField]
    public Image descriptionImage;

    bool purchased = false;

    private void Start()
    {
        GetComponentInParent<ShopCoinManager>().PurchaseFinished += PurchaseFinished;
        titleText.text = shopOptionData.title;
    }

    private void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionImage.sprite = shopOptionData?.correspondingImage;
        descriptionText.text = shopOptionData?.description;
        priceText.text = "PRICE: " + shopOptionData?.price.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GetComponentInParent<ShopCoinManager>().Purchase(shopOptionData.price, shopOptionData.title);
    }

    private void PurchaseFinished(object sender, ItemPurchasedEventArgs e)
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        if(gameObject.GetComponent<ShopItemTemplate>().shopOptionData.title == e.TitleText)
        {
            gameObject.SetActive(false);
            GetComponentInParent<GeneralShopManager>().AddWeaponToWeaponListHolder(shopOptionData.weaponSO);

            player.GetComponentInChildren<GunManager>()
                .AddGun(gameObject.GetComponent<ShopItemTemplate>().shopOptionData.weaponSO.prefab);

            //GameObject.FindGameObjectWithTag("SelectWeaponListHolder").GetComponent<SelectWeaponManager>().AddWeaponToList(shopOptionData.weaponSO);
            purchased = true;

            DataPersistenceManager.instance.SaveGame();
        }

    }

    public void LoadData(GameData data)
    {
        

        if (shopOptionData != null)
        {
            string id = shopOptionData.weaponSO.ItemName;

            if (shopOptionData.weaponSO.weaponSlot == WeaponSlot.Primary && data.primaryWeaponsPurchased.Count > 0)
            {
                data.primaryWeaponsPurchased.TryGetValue(id, out purchased);

            }
            if (shopOptionData.weaponSO.weaponSlot == WeaponSlot.Secondary && data.secondaryWeaponsPurchased.Count > 0)
            {
                data.secondaryWeaponsPurchased.TryGetValue(id, out purchased);
            }

            if (purchased)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void SaveData(GameData data)
    {
        if (shopOptionData != null)
        {
            string id = shopOptionData.weaponSO.ItemName;

            if (shopOptionData.weaponSO.weaponSlot == WeaponSlot.Primary)
            {
                if (data.primaryWeaponsPurchased.ContainsKey(id))
                {
                    data.primaryWeaponsPurchased.Remove(id);
                    //data.primaryWeaponsPurchased.Add(id, purchased);
                }
                data.primaryWeaponsPurchased.Add(id, purchased);
            }
            if (shopOptionData.weaponSO.weaponSlot == WeaponSlot.Secondary)
            {
                if (data.secondaryWeaponsPurchased.ContainsKey(id))
                {
                    data.secondaryWeaponsPurchased.Remove(id);
                    //data.secondaryWeaponsPurchased.Add(id, purchased);
                }
                data.secondaryWeaponsPurchased.Add(id, purchased);
            }
        }

        
    }
}



