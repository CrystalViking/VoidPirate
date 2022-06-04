using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weapon;
    [SerializeField] PlayerInventory playerInventory;
    [SerializeField] KeyCode itemPickupCode = KeyCode.E;
    [SerializeField] bool destroyOnPickUp = true;

    private bool isInRange;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(itemPickupCode) && isInRange)
        {
            playerInventory.AddItem(weapon);
            
            if(destroyOnPickUp)
            {
                Destroy(gameObject);
            }
            


        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
            isInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.name == "Player")
            isInRange = false;
    }
}
