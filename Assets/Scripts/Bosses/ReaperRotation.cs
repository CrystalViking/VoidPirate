using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperRotation : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (this.GetComponent<ReaperController>().health > 0)
        {
            if ((player.transform.position.x < transform.position.x))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
    }
}
