using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIOptionButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //public Image prompt;
    //public Image none;
    Image GetImage;

    void Start()
    {
        GetImage = GetComponentInChildren<Image>();
        GetImage.gameObject.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetImage.gameObject.SetActive(true);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        GetImage.gameObject.SetActive(false);
    }

    
}
