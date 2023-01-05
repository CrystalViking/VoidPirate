using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomObstacle : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }
    public GridController grid;
    public RandomSpawner spawner;
    private List<int> obstacleList = new List<int>();

    private void Start()
    {

    }

    public void SpawnObstacles()
    {
        int numberOfGenerationMethods = 8;
        int randomMethodNumber = Random.Range(1, numberOfGenerationMethods + 1);

        switch (randomMethodNumber)
        {
            case (1):
                obstacleList = new List<int>() { 115, 16, 17, 18, 79, 150, 21, 25, 56, 97, 28, };
                break;
            case (2):
                obstacleList = new List<int>() { 0, 28, 42, 56, 70, 84, 98, 112, 126, 140, 154, 168, 182, 196, 210, 224, 238, 252, 266 };
                break;
            case (3):
                obstacleList = new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 51, 75, 76, 77, 78, 79, 80, 105, 129, 130, 131, 132, 133, 134, 159, 183, 184, 199, 213, 214, 215, 216, 217, 218 };
                break;
            case (4):
                obstacleList = new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 39, 53, 54, 55, 56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 81, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105, 119, 133, 134, 135, 136, 137, 138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 165, 179, 180, 183, 184, 185, 186, 187, 188, 188, 189, 190, 191, 192, 193, 194, 195, 196, 197, 211, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239, 240, 241, 242, 243, 244, 245, 259, 273, 274, 275, 276, 277, 278, 279, 280, 281, 282, 283, 284, 285, 286, 287, 288, 289, 290, 291 };
                break;
            case (5):
                obstacleList = new List<int>() { 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 75, 76, 77, 78, 79, 80, 130, 131, 132, 133, 134, 159, 184, 185, 186, 187, 188, 189, 190, 191, 192, 193, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256, 257, 258, 259, 309, 310, 311, 312, 313, 314, 315, 316, 317, 318, 319, 320, 321, 322, 323, 324, 325, 326 };
                break;
            default:
                obstacleList = GetRandomCenteredObstacleList();
                break;

        }

        foreach (int pos in obstacleList)
        {
            GameObject go = Instantiate(spawner.spawnerData.itemToSpawn, grid.availablePoints[pos], Quaternion.identity, transform) as GameObject;
        }

        for (int i = obstacleList.Count - 1; i >= 0; i--)
        {
            grid.availablePoints.RemoveAt(obstacleList[i]);
        }
    }

    public List<int> GetRandomCenteredObstacleList()
    {
        List<int> obstacleList = new List<int>();
        int centerX = 13;
        int centerY = 7;
        int x, y;
        do
        {
            x = Random.Range(-3, 4);
            y = Random.Range(-3, 4);
        } while (Mathf.Abs(x) + Mathf.Abs(y) > 3 || (centerX + x == 13 && centerY + y == 7) || (centerX + x == 14 && centerY + y == 7));
        obstacleList.Add(centerY * 26 + centerX);
        obstacleList.Add(centerY * 26 + centerX + x);
        obstacleList.Add((centerY + y) * 26 + centerX);
        obstacleList.Add((centerY + y) * 26 + centerX + x);
        return obstacleList;
    }

}