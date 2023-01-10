using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GoToNextMenuOption : MonoBehaviour, IPointerClickHandler
{
    //[SerializeField]
    //Image correspondingImage;

    [SerializeField]
    GameObject currentMenu;

    [SerializeField]
    GameObject nextMenu;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(currentMenu != null)
            currentMenu.SetActive(false);
        if(nextMenu != null)
            nextMenu.SetActive(true);
    }
}
