using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudScript : MonoBehaviour
{
    [SerializeField] private TMP_Text currentHealthText;
    //[SerializeField] private TMP_Text maxHealthText;
    [SerializeField] private WeaponUI weaponUI;

    

    public void UpdateHealth(float currentHealth)
    {
        currentHealthText.text = currentHealth.ToString();
        //maxHealthText.text = maxHealth.ToString();
    }

    public void UpdateWeaponUI(WeaponSO newWeapon)
    {
        if (newWeapon != null)
            weaponUI.UpdateInfo(newWeapon.Icon, newWeapon.magazineSize, newWeapon.storedAmmo);
    }

    public void UpdateWeaponUI(WeaponSO weapon, int magAmmo, int ammoLeft)
    {
        if (weapon != null)
            weaponUI.UpdateInfo(weapon.Icon, magAmmo, ammoLeft);
    }

    public void UpdateWeaponAmmoInfo(int mag, int left)
    {
        weaponUI.UpdateAmmoInfo(mag, left);
    }

    

   


}
