using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Modules.AudioModule
{
    [Serializable]
    public struct Sound
    {
        [GUIColor(0f, 1f, 0)] [SerializeField] public SoundType Type;

        public SoundInfo SoundInfo;
    }
}