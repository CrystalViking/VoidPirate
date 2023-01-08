using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFreezer : MonoBehaviour
{
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        DisablePlayerMovementOnActiveCanvas();
    }


    private void OnDisable()
    {
        EnablePlayerMovementOnInactiveCanvas();
    }

    private void DisablePlayerMovementOnActiveCanvas()
    {
        if(gameObject.activeSelf)
        {
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>().DeactivateMovement();
        }
    }

    private void EnablePlayerMovementOnInactiveCanvas()
    {
        if(gameObject.activeSelf == false)
        {
            GameObject.FindGameObjectWithTag("Player").gameObject.GetComponent<PlayerMovement>().ActivateMovement();
        }
    }
}
