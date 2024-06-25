using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Lean.Pool;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Source.Modules.AudioModule.Scripts;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Source.Modules.AudioModule
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup _mixerGroup;
        [SerializeField] private Transform _sourceParent;
        [SerializeField] private AudioSourceHandler _audioSourceHandler;

        [BoxGroup("Music")] [SerializeField] private AudioMixerGroup _commonMusicGroup;
        [BoxGroup("Music")] [SerializeField] private AudioMixerGroup _musicGroup;
        [BoxGroup("SFX")] [SerializeField] private AudioMixerGroup _sfxGroup;
      

        public List<AudioSource> _audioSources = new();
        private readonly Dictionary<SoundType, AudioSource> _3dAudioSources = new();
        private SoundContainer _soundContainer;
        private Tween _fadeTween;

        public async void Initialize()
        {
            _soundContainer = await SoundContainer.GetInstanceAsync();
        }

        private void OnDestroy()
        {
            _fadeTween?.Kill(true);
        }
        

        public void Stop3DSound(SoundType soundType)
        {
            if (soundType == SoundType.None)
            {
                return;
            }

            if (_3dAudioSources.TryGetValue(soundType, out AudioSource audioSource) == false)
            {
                return;
            }

            if (audioSource == null)
            {
                return;
            }

            FadeVolume(audioSource, 0f, _soundContainer.Sounds.First(s => s.Type == soundType).SoundInfo.StereoData
                .FadeOutDuration, finished: () => audioSource.enabled = false);
        }

        public bool IsSoundPlaying(SoundType soundType)
        {
            Sound soundModel = _soundContainer.Sounds.First(s => s.Type == soundType);
            AudioSource audioSource = _audioSources.Find(s => s.clip == soundModel.SoundInfo.AudioClips.First());

            return audioSource != null &&
                   _audioSources.Any(x => soundModel.SoundInfo.AudioClips.Contains(x.clip) && x.isPlaying);
        }

        public void StopSound(SoundType soundType)
        {
            Sound soundModel = _soundContainer.Sounds.First(s => s.Type == soundType);
            _audioSources.Where(x => soundModel.SoundInfo.AudioClips.Contains(x.clip)).ForEach(x => x.Stop());
        }

        public async void PlaySoundByType(SoundType type, float delay = -1f, float volume = -1f, float pitch = -1f,
            bool isLoop = false, Transform parent = null)
        {
            if (type == SoundType.None)
            {
                return;
            }

            if (delay > 0f)
            {
                await UniTask.Delay(TimeSpan.FromSeconds(delay));
            }

            Sound soundModel = _soundContainer.Sounds.First(s => s.Type == type);

            if (parent != null)
            {
                Play3DSound(soundModel, volume, pitch, isLoop, parent);
                return;
            }

            PlaySound(soundModel, volume, pitch, isLoop);
        }

        private void Play3DSound(Sound sound, float volume = -1f, float pitch = -1f, bool isLoop = false,
            Transform parent = null)
        {
            _3dAudioSources.TryGetValue(sound.Type, out AudioSource source);

            float baseVolume = GetVolume(sound);

            float basePitch = GetPitch(sound);

            if (source == null)
            {
                source = CreateSoundSource(parent);

                _3dAudioSources.TryAdd(sound.Type, source);
            }

            if (source.enabled == false)
            {
                source.enabled = true;
            }

            if (sound.SoundInfo.Is3DSound)
            {
                source.spatialBlend = sound.SoundInfo.StereoData.SpatialBlend;
                source.rolloffMode = sound.SoundInfo.StereoData.VolumeRolloff;
                source.minDistance = sound.SoundInfo.StereoData.MinDistance;
                source.maxDistance = sound.SoundInfo.StereoData.MaxDistance;
                source.spread = sound.SoundInfo.StereoData.Spread;
            }

            source.clip = sound.SoundInfo.AudioClips[Random.Range(0, sound.SoundInfo.AudioClips.Length)];

            source.volume = baseVolume;

            if (volume > 0)
            {
                source.volume = volume;
            }

            source.pitch = basePitch;

            if (pitch > 0)
            {
                source.pitch = pitch;
            }

            if (isLoop)
            {
                source.loop = true;
            }
            

            source.outputAudioMixerGroup = GetAudioMixerGroup(sound.SoundInfo.MixerGroup);
             source.volume = 1;
            source.Play();
            FadeVolume(source, baseVolume, sound.SoundInfo.StereoData.FadeInDuration);
        }

        private void FadeVolume(AudioSource audioSource, float targetVolume, float fadeDuration, Action started = null,
            Action finished = null)
        {
            _fadeTween?.Kill(true);
            started?.Invoke();
            _fadeTween = audioSource.DOFade(targetVolume, fadeDuration).OnComplete(() => finished?.Invoke());
        }

        private float GetPitch(Sound sound)
        {
            return sound.SoundInfo.PitchData.ValType == ValueType.Constant
                ? sound.SoundInfo.PitchData.ConstantPitch
                : Random.Range(sound.SoundInfo.PitchData.MinPitch, sound.SoundInfo.PitchData.MaxPitch);
        }

        private float GetVolume(Sound sound)
        {
            return sound.SoundInfo.VolumeData.ValType == ValueType.Constant
                ? sound.SoundInfo.VolumeData.ConstantVolume
                : Random.Range(sound.SoundInfo.VolumeData.MinVolume, sound.SoundInfo.VolumeData.MaxVolume);
        }

        private void OnAudioSourceDespawn(AudioSourceHandler audioSourceHandler)
        {
            audioSourceHandler.DeSpawned -= OnAudioSourceDespawn;
            _audioSources.Remove(audioSourceHandler.AudioSource);
        }

        private void PlaySound(Sound sound, float volume = -1f, float pitch = -1f, bool isLoop = false)
        {
            AudioSourceHandler audioSourceHandler = LeanPool.Spawn(_audioSourceHandler, _sourceParent);
            AudioSource source = audioSourceHandler.AudioSource;
            _audioSources.Add(source);
            audioSourceHandler.DeSpawned += OnAudioSourceDespawn;
            source.outputAudioMixerGroup = _mixerGroup;
            source.playOnAwake = false;
            source.loop = false;
            source.spatialBlend = 0f;

            float baseVolume = GetVolume(sound);

            float basePitch = GetPitch(sound);

            source.clip = sound.SoundInfo.AudioClips[Random.Range(0, sound.SoundInfo.AudioClips.Length)];

            source.volume = baseVolume;

            if (volume > 0)
            {
                source.volume = volume;
            }

            source.pitch = basePitch;

            if (pitch > 0)
            {
                source.pitch = pitch;
            }

            if (isLoop)
            {
                source.loop = true;
            }
            
            source.outputAudioMixerGroup = GetAudioMixerGroup(sound.SoundInfo.MixerGroup);
            source.Play();
        }
        

        private AudioSource CreateSoundSource(Transform parent = null)
        {
            Transform targetParent = parent ? parent : _sourceParent;

            AudioSource newSource = targetParent.gameObject.AddComponent<AudioSource>();
            newSource.outputAudioMixerGroup = _mixerGroup;
            newSource.playOnAwake = false;
            newSource.loop = false;
            newSource.spatialBlend = 0f;
            return newSource;
        }

        private AudioMixerGroup GetAudioMixerGroup(MixerGroup mixerGroup)
        {
            switch (mixerGroup)
            {
                case MixerGroup.CommonMusic: return _commonMusicGroup;
                case MixerGroup.Music: return _musicGroup;
                case MixerGroup.SFX: return _sfxGroup;
            }

            return null;
        }
    }
}