using System;
using UnityEngine;

namespace Source.Modules.AudioModule
{
    [Serializable]
    public struct StereoData
    {
        [Range(0f, 1f)] public float SpatialBlend;
        public AudioRolloffMode VolumeRolloff;
        public float MinDistance;
        public float MaxDistance;
        [Range(0f, 360f)] public float Spread;
        public float FadeInDuration;
        public float FadeOutDuration;
    }
}