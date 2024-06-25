using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Modules.AudioModule
{
    [Serializable]
    public struct SoundInfo
    {
        [SerializeField] public AudioClip[] AudioClips;
        [SerializeField] public VolumeData VolumeData;
        [SerializeField] public PitchData PitchData;

        [ShowIf("@Is3DSound == true")] [SerializeField]
        public StereoData StereoData;

        [SerializeField] public MixerGroup MixerGroup;

        [OnValueChanged(nameof(OnSideEffectPropertyChange))] [SerializeField]
        public bool SideEffectSound;

        [SerializeField] public bool Is3DSound;

        [ShowIf("@SideEffectSound == true")] [SerializeField]
        public SideEffectData SideEffectData;

        private void OnSideEffectPropertyChange()
        {
            MixerGroup = SideEffectSound ? MixerGroup.SideChange : MixerGroup.SFX;
        }
    }
}