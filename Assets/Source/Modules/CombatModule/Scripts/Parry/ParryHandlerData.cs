using System;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts.Parry
{
    [Serializable]
    public struct ParryHandlerData
    {
        [field: SerializeField] public AnimationClip StrongParryAnimation;
        [field: SerializeField] public AnimationClip WeakParryAnimation;
        [field: SerializeField] public float CooldownToStrongParry;
    }
}