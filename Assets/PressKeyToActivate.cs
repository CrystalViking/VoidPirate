using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressKeyToActivate : MonoBehaviour
{
    public GameObject anyObject;
    public string key = "e";
    private bool inRange = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            inRange = false;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfButtonPressed();
    }

    void CheckIfButtonPressed()
    {
        if (inRange)
        {
            if (Input.GetKey(key))
            {
                anyObject.SetActive(true);
            }
        }
        
    }
}
