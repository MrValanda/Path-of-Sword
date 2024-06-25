using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Modules.AudioModule
{
    [Serializable]
    public struct PitchData
    {
        public ValueType ValType;

        [ShowIf("@ValType == ValueType.Constant")] [Range(1, 3f)]
        public float ConstantPitch;

        [ShowIf("@ValType == ValueType.Random")] [Range(0f, 3f)]
        public float MinPitch;

        [ShowIf("@ValType == ValueType.Random")] [Range(0f, 3f)]
        public float MaxPitch;
    }
}