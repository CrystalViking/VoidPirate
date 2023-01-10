using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject currentScreen;


    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadNewGameButton(string levelToLoad)
    {
        if(currentScreen != null)
            currentScreen.SetActive(false);
        loadingScreen.SetActive(true);

        DataPersistenceManager.instance.NewGame();

        StartCoroutine(LoadLevelASync(levelToLoad));
    }

    public void ContinueGameButton(string levelToLoad)
    {

    }

    public void LoadLevelButton(string levelToLoad)
    {
        if (currentScreen != null)
            currentScreen.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(levelToLoad));
    }


    // defining realm presumably in lobby ship
    public void DefineRealm(string name = "Estrella")
    {
        PlayerPrefs.SetString("nameOfBoss", name);         
    }

    public void WillBeBossRoom(string name = "No")
    {
        PlayerPrefs.SetString("isBoss", name);
    }

    public void ResetPrefs()
    {
        PlayerPrefs.SetInt("coinAmount", 0);
    }

    IEnumerator LoadLevelASync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        
        while(!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            loadingSlider.value = progressValue;
            yield return null;
        }
    }
}
