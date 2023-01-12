using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour, IDataPersistence
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

                FindObjectOfType<DeadBossChecker>().SetLevelFinished();

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

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(GameData data)
    {
        data.levelFinished = true;
    }
}
