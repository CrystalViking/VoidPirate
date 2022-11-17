using UnityEngine;

public interface IEnemyMovement
{
    public void SetPlayerTransform(Transform player);
    Vector3 MoveEnemy(Vector3 position, float speed);
}