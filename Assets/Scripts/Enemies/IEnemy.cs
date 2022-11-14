public interface IEnemy
{
    void Attack();
    void Die();
    void Follow();
    void Idle();
    void Wander();
    void TakeDamage(float damage);
    void SetUseRoomLogicTrue();
    void SetUseRoomLogicFalse();
    EnemyState GetEnemyState();

    void SetActiveBehaviourFalse();
    void SetActiveBehaviourTrue();

}