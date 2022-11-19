public interface IEnemy
{
    void TakeDamage(float damage);
    void SetUseRoomLogicTrue();
    void SetUseRoomLogicFalse();
    EnemyState GetEnemyState();

    void SetActiveBehaviourFalse();
    void SetActiveBehaviourTrue();
    bool HasFullHealth();

}