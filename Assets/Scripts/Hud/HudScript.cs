using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HudScript : MonoBehaviour
{
    [SerializeField] private TMP_Text currentHealthText;
    [SerializeField] private TMP_Text maxHealthText;
    [SerializeField] private WeaponUI weaponUI;

    

    public void UpdateHealth(float currentHealth, float maxHealth)
    {
        currentHealthText.text = currentHealth.ToString();
        maxHealthText.text = maxHealth.ToString();
    }

    public void UpdateWeaponUI(Weapon newWeapon)
    {
        if (newWeapon != null)
            weaponUI.UpdateInfo(newWeapon.Icon, newWeapon.magazineSize, newWeapon.storedAmmo);
    }

    public void UpdateWeaponAmmoInfo(int mag, int left)
    {
        weaponUI.UpdateAmmoInfo(mag, left);
    }

    

   


}
