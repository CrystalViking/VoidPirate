using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TravelToNextLevelButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    GameObject terminalWindow;

    [SerializeField]
    GameObject asyncManager;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (terminalWindow != null)
        {
            terminalWindow.SetActive(false);
        }
        asyncManager.GetComponent<AsyncLoader>().LoadLevel("TransitionScene");
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
