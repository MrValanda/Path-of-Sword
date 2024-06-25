using Source.Modules.AudioModule.Scripts;
using Source.Scripts.Utils.SO;
using UnityEngine;
using UnityEngine.Audio;

namespace Source.Modules.AudioModule
{
    [CreateAssetMenu(fileName = "AudioConfig", menuName = "App/Audio/" + nameof(AudioConfig))]
    public class AudioConfig : LoadableScriptableObject<AudioConfig>
    {
        public AudioMixer AudioMixer => _audioMixer;
        public SoundContainer SoundContainer => _soundContainer;
        
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private SoundContainer _soundContainer;
    }
}
