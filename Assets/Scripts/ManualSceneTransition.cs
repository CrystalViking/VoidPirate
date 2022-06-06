using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManualSceneTransition : MonoBehaviour
{
    [SerializeField] private int index;

    public void LoadScene()
    {
        SceneManager.LoadScene(index);
    }
}
