using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedkitPickup : MonoBehaviour
{

    [SerializeField] ScriptableMedkit medkit;
    [SerializeField] KeyCode itemPickupCode = KeyCode.E;
    [SerializeField] bool destroyOnPickUp = true;

    private bool isInRange;
    void Update()
    {
        if(Input.GetKeyDown(itemPickupCode) && isInRange)
        {
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>().Heal(medkit.hp_content);

            if (destroyOnPickUp)
            {
                Destroy(gameObject);
            }
        }
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
}
