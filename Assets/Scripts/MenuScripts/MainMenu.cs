using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string firstLevel;

    public string testLevel;

    public GameObject optionsScreen;

    public GameObject controlsScreen;

    [SerializeField] private GameObject continueGameButton;

    // Start is called before the first frame update
    void Start()
    {
        if(!DataPersistenceManager.instance.HasGameData())
        {
            continueGameButton.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartNewGame()
    {
        //SceneManager.LoadScene(firstLevel);

        //DataPersistenceManager.instance.NewGame();
    }

    public void ContinueGame()
    {

    }

    public void StartTestLevel()
    {
        SceneManager.LoadScene(testLevel);
    }

    public void OpenOptions()
    {
        optionsScreen.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsScreen.SetActive(false);
    }
    public void OpenControls()
    {
        controlsScreen.SetActive(true);
    }

    public void CloseControls()
    {
        controlsScreen.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quitting");
    }
}
