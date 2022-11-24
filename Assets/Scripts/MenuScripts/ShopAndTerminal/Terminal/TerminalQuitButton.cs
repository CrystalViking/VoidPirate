using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TerminalQuitButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject terminalWindow;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(terminalWindow != null)
            terminalWindow.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
