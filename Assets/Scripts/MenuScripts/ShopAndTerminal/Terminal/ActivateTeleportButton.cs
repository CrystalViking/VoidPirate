using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ActivateTeleportButton : MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private GameObject teleport;
    [SerializeField]
    GameObject terminalWindow;

    public string levelToLoad;
    public string realm;
    public string willBeBoss;

    public void OnPointerClick(PointerEventData eventData)
    {
        teleport.GetComponent<Teleport>().SetActive();
        teleport.GetComponent<Teleport>().SetLevelToLoad(levelToLoad, realm, willBeBoss);
        if (terminalWindow != null)
        {
            terminalWindow.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTerminalLevelToLoad(string levelToLoad, string realm, string willBeBoss)
    {
        this.levelToLoad = levelToLoad;
        this.realm = realm;
        this.willBeBoss = willBeBoss;
    }
}
