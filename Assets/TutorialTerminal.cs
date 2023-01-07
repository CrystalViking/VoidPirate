using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTerminal : MonoBehaviour
{
    bool isInRange;
    bool isActive = true;

    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;

    [SerializeField] GameObject terminalMenu;
    [SerializeField] GameObject tutorialCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
                tutorialCanvas.SetActive(false);
                terminalMenu.SetActive(true);
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
