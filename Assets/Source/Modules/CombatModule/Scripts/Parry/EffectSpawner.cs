using Lean.Pool;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts.Parry
{
    public class EffectSpawner : OptimizedMonoBehavior
    {
        [SerializeField] private Transform _effectPoint;
        [SerializeField] private ParticleSystem _effect;

        public void SpawnEffect()
        {
            LeanPool.Spawn(_effect, _effectPoint.position,Quaternion.identity).Play();
        }
    }
}