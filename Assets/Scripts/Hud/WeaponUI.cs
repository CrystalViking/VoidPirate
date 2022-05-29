using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeaponUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TMP_Text magazineSizeText;
    [SerializeField] private TMP_Text ammoLeftText;

    

    public void UpdateInfo(Sprite weaponIcon, int magazineSize, int ammoLeft)
    {
        icon.sprite = weaponIcon;
        magazineSizeText.text = magazineSize.ToString();
        ammoLeftText.text = ammoLeft.ToString();
    }

    public void UpdateAmmoInfo(int magazineSize, int ammoLeft)
    {
        magazineSizeText.text = magazineSize.ToString();
        ammoLeftText.text = ammoLeft.ToString();
    }
   
}
