using System;
using BehaviorDesigner.Runtime.Tasks;
using Lean.Pool;
using Source.Modules.BehaviorTreeModule;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class PlayVFXGameAction : IGameAction
    {
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private Transform _effectParent;
        [SerializeField] private Vector3 _effectScale;


        public PlayVFXGameAction(ParticleSystem effect, Transform effectParent, Vector3 effectScale)
        {
            _effect = effect;
            _effectParent = effectParent;
            _effectScale = effectScale;
        }

        public TaskStatus ExecuteAction()
        {
            ParticleSystem spawnedEffect = LeanPool.Spawn(_effect, _effectParent);
            spawnedEffect.transform.localScale = _effectScale;
            return TaskStatus.Success;
        }
    }
    
    
}