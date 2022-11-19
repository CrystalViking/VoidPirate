using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isPaused = false;


    [SerializeField] private GameObject hudCanvas = null;
    [SerializeField] private GameObject pauseCanvas = null;

    private void Start()
    {
        //SetActiveHud(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            SetActivePause(true);
            
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            SetActivePause(false);
            
        }
    }

    public void SetActiveHud(bool state)
    {
        hudCanvas.SetActive(state);
        pauseCanvas.SetActive(!state);
    }


    public void SetActivePause(bool state)
    {
        hudCanvas.SetActive(!state);
        pauseCanvas.SetActive(state);

        Time.timeScale = state ? 0 : 1;
        isPaused = state;
    }


    public void GoToMainMenu()
    {
        SetActivePause(false);
        SceneManager.LoadScene("MenuScene");
    }

    public void ContinueGame()
    {
        SetActivePause(false);
    }
}
