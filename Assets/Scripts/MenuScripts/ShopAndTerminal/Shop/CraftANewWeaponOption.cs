using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CraftANewWeaponOption : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    Image correspondingImage;

    [SerializeField]
    GameObject mainMenu;

    [SerializeField]
    GameObject weaponMenu;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(mainMenu != null)
            mainMenu.SetActive(false);
        if(weaponMenu != null)
            weaponMenu.SetActive(true);
    }
}
