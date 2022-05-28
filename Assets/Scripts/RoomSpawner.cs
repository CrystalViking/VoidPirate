using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;
    private RoomTemplates templates;
    private int rand;
    private bool spawned = false;

    /**
     *
     *  Door placement
     *
     *      ___ ___
     *     |   2   |
     *     3       4
     *     |___1___|
     *      
     *
     *  If you type "1" in spawnPoint's openingDirection,
     *  it will spawn there (on spawnpoint's location) a room
     *  with door on the bottom
     *
     */

    private void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.1f);
    }

    private void Spawn()
    {
        if (spawned == false)
        {

            if (openingDirection == 1)
            {
                // bottom door room
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position - new Vector3(5,5,0), templates.bottomRooms[rand].transform.rotation);
            }
            else if (openingDirection == 2)
            {
                // top door room
                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position - new Vector3(5,5,0), templates.topRooms[rand].transform.rotation);
            }
            else if (openingDirection == 3)
            {
                // left door room
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position - new Vector3(5,5,0), templates.leftRooms[rand].transform.rotation);
            }
            else if (openingDirection == 4)
            {
                // right door room
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position - new Vector3(5,5,0), templates.rightRooms[rand].transform.rotation);
            }

            spawned = true;
        }


    }
    private void onTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Spawn Point")){
            Destroy(gameObject);
        }
     }
}
