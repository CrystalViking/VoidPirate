using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour
{
    GameObject player;
    [SerializeField] string direction;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (this.GetComponent<IEnemy>().GetEnemyState() != EnemyState.Die) 
        {
            if (direction == "left")
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
            else
            {
                if ((player.transform.position.x < transform.position.x))
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }
            }
        }     
    }
}
