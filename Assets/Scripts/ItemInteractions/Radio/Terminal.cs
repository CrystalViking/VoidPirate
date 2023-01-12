using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Terminal : MonoBehaviour, IDataPersistence
{

    bool isInRange;
    bool isActive = true;

    

    [SerializeField] protected KeyCode itemInteractionCode = KeyCode.E;

    [SerializeField] GameObject terminalMenu;

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
                if(SceneManager.GetActiveScene().name == "MainGameScene")
                {
                    DataPersistenceManager.instance.LoadGame();
                    DataPersistenceManager.instance.SaveGame();
                }
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

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(GameData data)
    {
        if(SceneManager.GetActiveScene().name == "MainGameScene")
        {
            data.lobbyBossState = LobbyBossState.bossLocationUnlocked;
            data.levelFinished = true;
        }
            
    }
}
