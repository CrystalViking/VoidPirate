using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxPickup : MonoBehaviour
{
    [SerializeField] ScriptableAmmoBox ammoBox;
    [SerializeField] KeyCode itemPickupCode = KeyCode.E;
    [SerializeField] bool destroyOnPickUp;

    private bool isInRange;
    private WeaponShooting weaponInterface;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        GetReferences();
    }

    // Update is called once per frame
    void Update()
    {
        PickUp();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInRange = false;
    }

    private void ReplenishAmmo()
    {
        weaponInterface.AddAmmoToCurrentWeaponType(ammoBox.ammoType, (int)Random.Range(ammoBox.minAmmo, ammoBox.maxAmmo));
    }


    private void GetReferences()
    {
        player = GameObject.Find("Player");
        weaponInterface = player.GetComponent<WeaponShooting>();
        isInRange = false;
    }


    private void PickUp()
    {
        if(Input.GetKeyDown(itemPickupCode) && isInRange)
        {
            ReplenishAmmo();

            if (destroyOnPickUp)
            {
                Destroy(gameObject); 
            }
        }
    }
}
