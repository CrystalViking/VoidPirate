using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class TransitionLevelManager : MonoBehaviour, IDataPersistence
{

    public int day;

    public bool newGame;

    public bool sceneSet = false;

    public static string BULLET_HELL_SCENE = "BulletHellScene";
    public static string LOBBY_SHIP = "LobbyShipFinal";

    public int spaceshipProbability;
    public int bulletHellProbability;

    public LobbyBossState bossState;
    public LobbyTravelState travelState;

    [SerializeField]
    private GameObject asyncManager;

    System.Random rand = new System.Random();

    [SerializeField]
    private UnityEvent<string> onDayChanged;


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        DataPersistenceManager.instance.LoadGame();
        CalculateShipAndBulletHell();
    }

    private void CalculateShipAndBulletHell()
    {
        int spaceshipRandValue;
        int bulletHellRandValue;

        //test code
        newGame = false;
        //bossState = LobbyBossState.bossLocationUnknown;
        //end test code

        if(newGame == false)
        {
            if(bossState == LobbyBossState.bossLocationUnknown)
            {
                if(spaceshipProbability < 100)
                {
                    spaceshipProbability += 25;
                }

                spaceshipRandValue = rand.Next(0, 101);

                if(spaceshipProbability > spaceshipRandValue)
                {
                    spaceshipProbability = 0;
                    travelState = LobbyTravelState.OnSpaceshipLevel;
                }
                else
                {
                    travelState = LobbyTravelState.OnDungeonLevel;
                }
            }
            else if(bossState == LobbyBossState.bossLocationSet)
            {
                travelState = LobbyTravelState.OnBossLevel;
            }
            else if(bossState == LobbyBossState.bossDefeated)
            {
                bossState = LobbyBossState.bossLocationUnknown;
                travelState = LobbyTravelState.OnDungeonLevel;
                if (sceneSet == false)
                {
                    sceneSet = true;
                    StartCoroutine(Transit(LOBBY_SHIP));
                }
            }

            day++;
            onDayChanged?.Invoke("DAY " + day.ToString());

            if (bulletHellProbability < 100)
            {
                bulletHellProbability += 30;
            }

            bulletHellRandValue = rand.Next(0, 101);

            if (bulletHellProbability > bulletHellRandValue)
            {
                bulletHellProbability = 0;
                DataPersistenceManager.instance.SaveGame();
                if(sceneSet == false)
                {
                    sceneSet = true;
                    StartCoroutine(Transit(BULLET_HELL_SCENE));
                }
                
            }

        }
        else
        {
            onDayChanged?.Invoke("DAY " + day.ToString());
        }
        DataPersistenceManager.instance.SaveGame();
        if(sceneSet == false)
            StartCoroutine(Transit(LOBBY_SHIP));
    }

    IEnumerator Transit(string level)
    {
        yield return new WaitForSeconds(10f);

        asyncManager.GetComponent<AsyncLoader>().LoadLevel(level);
        //StartCoroutine(loader.LoadScene("LobbyShipFinal"));
    }


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void LoadData(GameData data)
    {
        newGame = data.newGame;
        spaceshipProbability = data.spaceshipProbability;
        bulletHellProbability = data.bulletHellProbability;
        bossState = data.lobbyBossState;
        day = data.day;
    }

    public void SaveData(GameData data)
    {
        data.day = day;
        data.lobbyTravelState = travelState;
        data.spaceshipProbability = spaceshipProbability;
        data.bulletHellProbability = bulletHellProbability;
        data.lobbyBossState = bossState;
    }
}
