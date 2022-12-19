using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct Spawnable
    {
        public string Name;
        public GameObject gameObject;
        public float weight;
    }
    public List<Spawnable> items = new List<Spawnable>();
    float totalWeight;
    private int shouldBeEstrella;
    private int shouldBeReaper;
    private int shouldBeAtaros;

    private void Awake() {
        totalWeight = 0;

        foreach(var spawnable in items)
        {
            totalWeight += spawnable.weight;
        }
    }

    private void Start() {
        shouldBeEstrella = PlayerPrefs.GetInt("shouldBeEstrella", 1);
        shouldBeReaper = PlayerPrefs.GetInt("shouldBeReaper", 1);
        shouldBeAtaros = PlayerPrefs.GetInt("shouldBeAtaros", 1);
        float pick = Random.value * totalWeight;
        int chosenIndex = 0;
        float cumulativeWeight = items[0].weight;

        if (items[0].Name == "Estrella" && !(shouldBeEstrella == 1 && shouldBeReaper == 1 && shouldBeAtaros == 1))
        {
            if(shouldBeEstrella == 1)
            {
                GameObject i = Instantiate(items[0].gameObject, transform.position, Quaternion.identity, transform) as GameObject;
            }
            else if(shouldBeReaper == 1)
            {
                GameObject i = Instantiate(items[1].gameObject, transform.position, Quaternion.identity, transform) as GameObject;
            }
            else if(shouldBeAtaros == 1)
            {
                GameObject i = Instantiate(items[2].gameObject, transform.position, Quaternion.identity, transform) as GameObject;
            }
        }
        else
        {

            while (pick > cumulativeWeight && chosenIndex < items.Count - 1)
            {
                chosenIndex++;
                cumulativeWeight += items[chosenIndex].weight;
            }
            GameObject i = Instantiate(items[chosenIndex].gameObject, transform.position, Quaternion.identity, transform) as GameObject;
        }
    }

}
