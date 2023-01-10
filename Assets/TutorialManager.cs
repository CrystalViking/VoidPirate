using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TutorialManager : MonoBehaviour
{
    public GameObject[] tutorialObjectsList;
    public GameObject[] tutorialCanvasList;
    public GameObject helpCanvas;
    public bool teleportActivated = false;
    public Teleport teleportScript;

    private bool firstVisited = false;
    private bool secondVisited;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckTargets();
        MarkTargets();
    }
    private void CheckTargets()
    {
        if (tutorialCanvasList[0].activeSelf)
            firstVisited = true;
        if (tutorialCanvasList[1].activeSelf)
            secondVisited = true;
    }

    private void MarkTargets()
    {
        if (firstVisited == false)
            TargetIndicator.instance.MarkTarget(tutorialObjectsList[0].transform);
        if (firstVisited == true)
            TargetIndicator.instance.MarkTarget(tutorialObjectsList[1].transform);
        if (teleportActivated)
            TargetIndicator.instance.MarkTarget(tutorialObjectsList[2].transform);
    }

    public void ActivateTutorialTeleport()
    {
        if (firstVisited == true && secondVisited == true)
        {
            teleportScript.GetComponent<Teleport>().SetActive();
            teleportActivated = true;
            helpCanvas.SetActive(true);
        }
    }
}
