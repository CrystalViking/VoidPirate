using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    public PlayerMovement PlayerMovement { get; private set; }
    public PlayerCurrency PlayerCurrency { get; private set; }
    public PlayerRenderer PlayerRenderer { get; private set; }
    public UIManager UiManager { get; private set; }
    public IActorStats PlayerStats { get; private set; } 
    public HudScript PlayerHudScript { get; private set; }
    public WeaponInventory PlayerInventory { get; private set; }
    public EquipmentManager EquipmentManager { get; private set; }
    public WeaponShooting WeaponShooting { get; private set; }
    public WeaponParent WeaponParent { get; private set; }
    public HudScript HudScript { get; private set; }


    private void Awake()
    {
        GetReferences();
    }


    void Start()
    {
           
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetReferences()
    {
        PlayerMovement = GetComponent<PlayerMovement>();
        PlayerCurrency = GetComponent<PlayerCurrency>();
        PlayerRenderer = GetComponent<PlayerRenderer>();
        UiManager = GetComponent<UIManager>();
        PlayerStats = GetComponent<PlayerStats>();
        EquipmentManager = GetComponent<EquipmentManager>();
        PlayerInventory = GetComponent<WeaponInventory>();  
        WeaponShooting = GetComponent<WeaponShooting>();
        WeaponParent = GetComponent<WeaponParent>();
        HudScript = GetComponent<HudScript>();
    }

}
