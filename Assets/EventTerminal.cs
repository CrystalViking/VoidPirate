using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventTerminal : MonoBehaviour
{
    bool isInRange;
    bool isActive = true;
    public TMP_InputField inputField;

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
                inputField.Select();
                inputField.ActivateInputField();

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
