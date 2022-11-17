public interface IStatModApplier
{
    bool Expired { get; }
    StatModApplicationType ModApplicationType { get; }

    void ApplyModifier(ref StatModifier statModifier, ref CharacterStat characterStat, StatModApplicationType speedModApplicationType);
    void ApplyModifierForSeconds(ref StatModifier statModifier, ref CharacterStat characterStat, StatModApplicationType speedModApplicationType, float seconds);
    void RemoveModifer(ref StatModifier statModifier, ref CharacterStat characterStat, StatModApplicationType speedModApplicationType);
}