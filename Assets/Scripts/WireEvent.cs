using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireEvent : MonoBehaviour
{
    bool isInRange;
    bool isActive = true;

    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;

    [SerializeField] GameObject eventTerminalCanvas;

    void Start()
    {

    }

    void Update()
    {
        InteractOnAction();
    }

    public void InteractOnAction()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(itemInteractionCode) && isActive)
            {
                eventTerminalCanvas.SetActive(true);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInRange = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            isInRange = false;
    }
}
