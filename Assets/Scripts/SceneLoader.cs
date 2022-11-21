using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    public string sceneName;
    // Start is called before the first frame update
    private void Start()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
    }
    public IEnumerator LoadScene()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}
