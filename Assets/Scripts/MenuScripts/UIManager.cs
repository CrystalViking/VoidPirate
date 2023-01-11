using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private bool isPaused;


    [SerializeField] private GameObject hudCanvas = null;
    [SerializeField] private GameObject pauseCanvas = null;

    private void Start()
    {
        //SetActiveHud();
        SetPauseInactive();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            //SetActivePause(true);
            SetPauseActive();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            //SetActivePause(false);
            SetPauseInactive();
        }
        if(pauseCanvas != null)
        {
            if(pauseCanvas.activeSelf)
            {
                isPaused = true;
            }
            else
            {
                isPaused=false;
            }
        }
        
    }

    public void SetActiveHud()
    {
        if(hudCanvas != null)
            hudCanvas?.SetActive(true);


        isPaused = false;
        //pauseCanvas?.SetActive(!state);
    }

    public void SetPauseActive()
    {
        if (hudCanvas != null)
            hudCanvas.SetActive(false);
        if (pauseCanvas != null)
            pauseCanvas.SetActive(true);

        //isPaused = true;
        Time.timeScale = 0f;
    }

    public void SetPauseInactive()
    {
        if (hudCanvas != null)
            hudCanvas.SetActive(true);
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);

        //isPaused = false;
        Time.timeScale = 1f;
    }


    public void SetActivePause(bool state)
    {
        if (hudCanvas != null)
            hudCanvas?.SetActive(!state);
        if (pauseCanvas != null)
            pauseCanvas?.SetActive(state);

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
        //SetActivePause(false);
        if(isPaused)
            SetPauseInactive();
    }
}
