using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RadioController : MonoBehaviour
{
    bool isInRange;
    bool isActive = false;
    public Radio radio;


    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;
    [SerializeField] protected KeyCode itemNextStationCode = KeyCode.RightArrow;
    [SerializeField] protected KeyCode itemPreviousStationCode = KeyCode.LeftArrow;
    // Start is called before the first frame update
    void Start()
    {
        radio = GetComponent<Radio>();
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
        if(isInRange)
        {
            if (Input.GetKeyDown(itemInteractionCode))
            {
                if (!isActive)
                {
                    StartCoroutine(activateRadio());
                }
                else
                {
                    StartCoroutine(deactivateRadio());
                }
            }
            if (Input.GetKeyDown(itemNextStationCode) && isActive)
            {
                radio.NavigateStations(true);
            }

            if(Input.GetKeyDown(itemPreviousStationCode) && isActive)
            {
                radio.NavigateStations(false);
            }
        }
        

    }
    IEnumerator activateRadio()
    {
        isActive = true;
        yield return new WaitForSeconds(0.5f);
        radio.PlayRadio();
    }

    IEnumerator deactivateRadio()
    {
        isActive = false;
        yield return new WaitForSeconds(0.5f);
        radio.StopRadio();
    }


}
