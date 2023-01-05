using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class MenuItemTemplate : MonoBehaviour, IPointerEnterHandler
{

    [SerializeField]
    MenuItemSO optionData;

    //public TMP_Text titleText;
    public TMP_Text descriptionText;

    [SerializeField]
    public Image descriptionImage;

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionImage.sprite = optionData.correspondingImage;
        descriptionText.text = optionData.description;
    }
}


