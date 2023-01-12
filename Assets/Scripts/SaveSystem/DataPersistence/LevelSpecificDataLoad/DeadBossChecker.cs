using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadBossChecker : MonoBehaviour, IDataPersistence
{
    private bool levelFinished;
    private LobbyBossState bossState;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




    public void SetBossDead()
    {
        levelFinished = true;
        bossState = LobbyBossState.bossDefeated;
    }

    public void SetLevelFinished()
    {
        levelFinished = true;
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(GameData data)
    {
        data.levelFinished = levelFinished;
        data.lobbyBossState = bossState;
    }
}
