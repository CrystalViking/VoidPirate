using UnityEngine;

public class MeleeEnemyMovement : MonoBehaviour, IEnemyMovement
{
    Transform player;

    public Vector3 MoveEnemy(Vector3 position, float speed)
    {
        position = Vector2.MoveTowards(position, player.position, speed * Time.deltaTime);
        return position;
    }

    public void SetPlayerTransform(Transform player)
    {
        this.player = player;
    }
}