using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using Source.Scripts.VisitableComponents;
using UnityEngine;

namespace Source.Scripts.States
{
    public class ProtectionState : State
    {
        [SerializeField, Range(0, 1)] private float _damageReduce;
        
        private Animator _animator;
        private static readonly int IsProtection = Animator.StringToHash("IsProtection");
        private float _previousReduce;
        
        private void OnEnable()
        {
            _animator ??= _entity.Get<AnimationHandler>().Animator;
            EntityCurrentStatsData entityCurrentStatsData = _entity.AddOrGet<EntityCurrentStatsData>();
            _previousReduce = entityCurrentStatsData.DamageReducePercent;
            entityCurrentStatsData.DamageReducePercent = _damageReduce;
            _animator.SetBool(IsProtection,true);
        }

        private void OnDisable()
        {
            _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent = _previousReduce;
            _animator.SetBool(IsProtection,false);
        }
    }
}