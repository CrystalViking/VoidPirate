using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopLoader : MonoBehaviour
{
    private void OnEnable()
    {
        DataPersistenceManager.instance.LoadGame();
    }
}
