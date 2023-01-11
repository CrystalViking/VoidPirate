using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTeleport : MonoBehaviour
{
    bool isInRange;
    bool isActive = true;

    [SerializeField]
    private SceneInfo sceneInfo;

    [SerializeField] string location_name;

    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;

    public GameObject helpCanvas;
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
                sceneInfo.isEventOn = false;
                DataPersistenceManager.instance.SaveGame();
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
