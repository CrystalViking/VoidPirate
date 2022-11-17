public interface IEnemyCalculations
{
    float CalculateDistanceFromPlayer();
    float CalculateHealthPercentage(float health);
    bool CanAttack();
    bool IsInAttackRange();
    bool IsInLineOfSight();
    bool IsItTimeToAttack();
    void SetEnemyData(EnemyData enemyData);

    void SetNextAttackTime();
}