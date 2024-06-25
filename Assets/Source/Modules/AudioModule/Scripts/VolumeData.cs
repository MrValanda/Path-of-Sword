using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Modules.AudioModule
{
    [Serializable]
    public struct VolumeData
    {
        public ValueType ValType;

        [ShowIf("@ValType == ValueType.Constant")] [Range(0f, 1f)]
        public float ConstantVolume;

        [ShowIf("@ValType == ValueType.Random")] [Range(0f, 1f)]
        public float MinVolume;

        [ShowIf("@ValType == ValueType.Random")] [Range(0f, 1f)]
        public float MaxVolume;
    }
}