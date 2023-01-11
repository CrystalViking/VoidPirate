using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminalLevelLogic : MonoBehaviour
{
    [SerializeField]
    private GameObject travelToNextLevelButton;

    [SerializeField]
    private GameObject activateTeleportButton;

    [SerializeField]
    private GameObject goToEstrellaButton;

    [SerializeField]
    private GameObject goToReaperButton;

    [SerializeField]
    private GameObject goToAtarosButton;

    LobbyLevelManager lobbyLevelManager;


    public string levelToLoad;
    public string realm;
    public string willBeBoss;

    private bool teleportButtonActive;
    private bool nextLevelButtonActive;

    private void OnEnable()
    {
        if(teleportButtonActive)
        {
            activateTeleportButton.SetActive(true);
            activateTeleportButton
                .GetComponent<ActivateTeleportButton>()
                .SetTerminalLevelToLoad(levelToLoad, realm, willBeBoss);
        }
        if(nextLevelButtonActive)
        {
            travelToNextLevelButton.SetActive(true);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ActivateTeleportButton()
    {
        teleportButtonActive = true;
    }

    public void ActivateNextLevelButton()
    {
        nextLevelButtonActive = true;
    }

    public void SetTerminalLevelToLoad(string levelToLoad, string realm, string willBeBoss)
    {
        this.levelToLoad = levelToLoad;
        this.realm = realm;
        this.willBeBoss = willBeBoss;

        ActivateTeleportButton();
    }


}
