public interface IStatModApplier
{
    bool Expired { get; }
    StatModApplicationType ModApplicationType { get; }
    void SetModifier(float value, StatModType statModType, ref CharacterStat characterStat, StatModApplicationType statModApplicationType);
    void ApplyModifier();
    void ApplyModifierForSeconds(float seconds);
    void RemoveModifer();
}