using System;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Modules.AudioModule
{
    public class AudioSourceHandler : MonoBehaviour
    {
        public event Action<AudioSourceHandler> DeSpawned;
        [field: SerializeField] public AudioSource AudioSource { get; private set; }


        private void FixedUpdate()
        {
            if (AudioSource.isPlaying == false)
            {
                DeSpawned?.Invoke(this);
                LeanPool.Despawn(this);
            }
        }

        [Button]
        public bool IsPlaying()
        {
            return AudioSource.isPlaying;
        }
    }
}