using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChairTeleport : MonoBehaviour
{
    bool isInRange;
    bool isActive = true;

    [SerializeField] string location_name;

    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;

    public GameObject helpCanvas;


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
            helpCanvas.SetActive(true);
            if (Input.GetKeyDown(itemInteractionCode) && isActive)
            {
                StartCoroutine(SceneLoader.instance.LoadScene(location_name));
            }
        }
        else if (!isInRange)
        {
            helpCanvas.SetActive(false);
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
