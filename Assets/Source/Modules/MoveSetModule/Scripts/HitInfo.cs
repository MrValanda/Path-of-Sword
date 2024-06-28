using System;
using Source.Modules.AudioModule;
using UnityEngine;

namespace Source.Modules.MoveSetModule.Scripts
{
    [Serializable]
    public struct HitInfo
    {
        [field: SerializeField, Min(1)] public int NumberOfHitsPerUnit { get; private set; }
        [field: SerializeField, Min(0)] public float DelayBetweenHits { get; private set; }
        [field: SerializeField, Min(0)] public float Damage { get; private set; }
        [field: SerializeField] public float ParryBackForce { get; private set; }
        [field: SerializeField] public float LossStaminaAfterParry { get; private set; }
        [field: SerializeField] public float LossStaminaWhenEntityParry { get; private set; }
        [field: SerializeField] public SoundType WhooshSoundsType { get; private set; }
    }
}