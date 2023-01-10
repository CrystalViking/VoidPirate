using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateInRange : MonoBehaviour
{
    bool isInRange;

    public GameObject anyObject;
    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;


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
            if (Input.GetKeyDown(itemInteractionCode))
            {
                anyObject.SetActive(true);
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
