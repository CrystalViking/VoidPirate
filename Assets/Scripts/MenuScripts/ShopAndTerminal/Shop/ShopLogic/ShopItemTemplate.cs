using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopItemTemplate : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    public ShopWeaponSO shopOptionData;


    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;

    [SerializeField]
    public Image descriptionImage;


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
            player.GetComponentInChildren<GunManager>()
                .AddGun(gameObject.GetComponent<ShopItemTemplate>().shopOptionData.weaponSO.prefab);

            //GameObject.FindGameObjectWithTag("SelectWeaponListHolder").GetComponent<SelectWeaponManager>().AddWeaponToList(shopOptionData.weaponSO);
            GetComponentInParent<GeneralShopManager>().AddWeaponToWeaponListHolder(shopOptionData.weaponSO);
        }

    }


}



