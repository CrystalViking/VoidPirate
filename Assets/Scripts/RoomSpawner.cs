using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    public int openingDirection;

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

    private void Update()
    {

        if (openingDirection == 1) {
            // bottom door room
         } else if (openingDirection == 2) {
            // top door room
         } else if (openingDirection == 3) {
            // left door room
         } else if (openingDirection == 4) {
            // right door room
         }

    }
}
