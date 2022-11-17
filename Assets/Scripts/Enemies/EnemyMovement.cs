using UnityEngine;

public abstract class EnemyMovement : IEnemyMovement
{
    public Transform player;

    public Vector3 MoveEnemy(Vector3 position, float speed)
    {
        throw new System.NotImplementedException();
    }

    public void SetPlayerTransform(Transform player)
    {
        Debug.Log("Enemy: Set Player Transform");
    }

   
}