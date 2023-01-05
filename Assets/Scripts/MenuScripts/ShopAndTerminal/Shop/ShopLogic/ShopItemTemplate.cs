using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ShopItemTemplate : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField]
    ShopWeaponSO shopOptionData;


    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text priceText;

    [SerializeField]
    public Image descriptionImage;


    private void Start()
    {
        titleText.text = shopOptionData.title;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionImage.sprite = shopOptionData?.correspondingImage;
        descriptionText.text = shopOptionData?.description;
        priceText.text = "PRICE: " + shopOptionData?.price.ToString();
    }
}
