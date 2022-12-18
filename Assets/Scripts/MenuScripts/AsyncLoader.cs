using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AsyncLoader : MonoBehaviour
{
    [Header("Menu Screens")]
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private GameObject mainMenu;


    [Header("Slider")]
    [SerializeField] private Slider loadingSlider;

    public void LoadLevelButton(string levelToLoad)
    {
        mainMenu.SetActive(false);
        loadingScreen.SetActive(true);

        StartCoroutine(LoadLevelASync(levelToLoad));
    }

    public void DefineRealm(string name = "Chaos")
    {
        PlayerPrefs.SetInt("shouldBeEstrella", 0);
        PlayerPrefs.SetInt("shouldBeReaper", 0);
        PlayerPrefs.SetInt("shouldBeAtaros", 0);
        if (name == "Estrella")
            PlayerPrefs.SetInt("shouldBeEstrella", 1);
        else if (name == "Reaper")
            PlayerPrefs.SetInt("shouldBeReaper", 1);
        else if (name == "Ataros")
            PlayerPrefs.SetInt("shouldBeAtaros", 1);
        else if(name == "Chaos")
        {
            PlayerPrefs.SetInt("shouldBeEstrella", 1);
            PlayerPrefs.SetInt("shouldBeReaper", 1);
            PlayerPrefs.SetInt("shouldBeAtaros", 1);
        }
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
