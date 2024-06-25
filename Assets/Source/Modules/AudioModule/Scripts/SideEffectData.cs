using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Modules.AudioModule
{
    [Serializable]
    public struct SideEffectData
    {
        public ValueType PercentValueType;

        [ShowIf("@PercentValueType == ValueType.Constant")] [Range(0f, 100f)]
        public float ConstantSidePercent;

        [ShowIf("@PercentValueType == ValueType.Random")] [Range(0f, 100f)]
        public float MinSidePercent;

        [ShowIf("@PercentValueType == ValueType.Random")] [Range(0f, 100f)]
        public float MaxSidePercent;

        public ValueType TimeValueType;

        [ShowIf("@TimeValueType == ValueType.Constant")] [Range(0f, 1f)]
        public float ConstantSideTime;

        [ShowIf("@TimeValueType == ValueType.Random")] [Range(0f, 1f)]
        public float MinSideTime;

        [ShowIf("@TimeValueType == ValueType.Random")] [Range(0f, 1f)]
        public float MaxSideTime;
    }
}