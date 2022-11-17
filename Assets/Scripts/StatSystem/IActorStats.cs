public interface IActorStats
{
    void CheckHealth();
    void Die();
    float GetHealth();
    float GetSpeed();
    void Heal(float heal);
    void SetHealthTo(float healthToSetTo);
    void TakeDamage(float damage);
}