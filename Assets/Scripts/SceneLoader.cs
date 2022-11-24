using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public float transitionTime;
    public Animator crossFade;
    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
    }
    public IEnumerator LoadScene(string sceneName)
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator LoadScene(string sceneName, float transitionTime)
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }



}
