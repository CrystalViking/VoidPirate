using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtarosRotation : MonoBehaviour
{
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (this.GetComponent<AtarosController>().health > 0)
        {
            if ((player.transform.position.x > transform.position.x))
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
            }
        }
        else
        {
            try
            {
                Transform red = transform.GetChild(4);
                Transform blue = transform.GetChild(5);
                if ((player.transform.position.x > transform.position.x))
                {
                    red.parent = null;
                    blue.parent = null;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                    red.parent = transform;
                    blue.parent = transform;
                }
                else
                {
                    red.parent = null;
                    blue.parent = null;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    red.parent = transform;
                    blue.parent = transform;
                }
            }
            catch { }
        }
    }
}
