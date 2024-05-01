using UnityEngine;

namespace Source.Scripts.Setups
{
    [CreateAssetMenu(fileName = "AbilitySetup", menuName = "Setups/Ability/AbilitySetup")]
    public class AbilitySetup : BaseAbilitySetup
    {
        [field: SerializeField] public float CooldownAbility { get; private set; }
        [field: SerializeField] public float ChanceToUse { get; private set; }
        [field: SerializeField] public float CooldownToUseAbility { get; private set; }
    }
}