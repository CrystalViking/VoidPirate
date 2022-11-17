using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class PickableItem : MonoBehaviour
{
    protected bool isInRange;

    
    [SerializeField] protected KeyCode itemPickupCode = KeyCode.E;
    [SerializeField] protected bool destroyOnPickUp = true;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInRange = true;
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInRange = false;
    }


    

    public virtual void InteractOnPickup()
    {
        if (Input.GetKeyDown(itemPickupCode) && isInRange)
        {
            Debug.Log("PickableItem interaction");

            if (destroyOnPickUp)
            {
                Destroy(gameObject);
            }
        }
    }
}
