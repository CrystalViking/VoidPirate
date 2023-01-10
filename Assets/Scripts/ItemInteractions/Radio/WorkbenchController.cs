using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkbenchController : MonoBehaviour, IInteractable
{
    bool isInRange;
    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;

    public GameObject shopCanvas;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InteractOnAction();
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

    public void InteractOnAction()
    {
        if (isInRange)
        {
            if (Input.GetKeyDown(itemInteractionCode))
                shopCanvas.SetActive(true);
        }

    }

    
}
