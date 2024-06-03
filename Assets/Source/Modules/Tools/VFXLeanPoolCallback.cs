using System;
using Lean.Pool;
using UnityEngine;

namespace Source.Modules.Tools
{
    [RequireComponent(typeof(ParticleSystem))]
    public class VFXLeanPoolCallback : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;

        private void OnValidate()
        {
            _particleSystem ??= GetComponent<ParticleSystem>();
        }

        private void OnParticleSystemStopped()
        {
            LeanPool.Despawn(_particleSystem);
        }
    }
}
