using Source.Modules.EnemyModule.Scripts.Setups;
using Source.Scripts.AnimationEventListeners;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.Abilities
{
    public struct AbilityDataContainer
    {
        public AbilityEventListener AbilityEventListener;
        public Animator Animator;
        public BaseAbilitySetup AbilitySetup;
     
    }
}