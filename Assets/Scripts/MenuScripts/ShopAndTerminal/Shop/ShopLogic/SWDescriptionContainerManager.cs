using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SWDescriptionContainerManager : MonoBehaviour
{

    public GameObject player;

    public TMP_Text primaryWeaponInfo;
    public TMP_Text secondaryWeaponInfo;

    private void OnEnable()
    {
        SetWeaponInfo();
    }

    private void SetWeaponInfo()
    {
        string primary = GameObject.FindGameObjectWithTag("Player")
            .GetComponentInChildren<GunManager>()
            .GetWeaponNamePrimary();
        string secondary = GameObject.FindGameObjectWithTag("Player")
            .GetComponentInChildren<GunManager>()
            .GetWeaponNameSecondary();

        primaryWeaponInfo.text = "PRIMARY: " + primary;
        secondaryWeaponInfo.text = "SECONDARY: " + secondary;
    }

    public void SetPrimaryInfoString(string primary)
    {
        primaryWeaponInfo.text = "PRIMARY: " + primary;
    }

    public void SetSecondaryInfoString(string secondary)
    {
        secondaryWeaponInfo.text = "SECONDARY: " + secondary;
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
