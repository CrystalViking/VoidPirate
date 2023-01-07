using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadSceneButton(string sceneToLoad)
    {
        StartCoroutine(SceneLoader.instance.LoadScene(sceneToLoad));
    }

}
