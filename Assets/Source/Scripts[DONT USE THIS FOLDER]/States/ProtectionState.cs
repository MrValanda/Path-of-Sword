using System;
using Source.Modules.CombatModule.Scripts;
using Source.Scripts.EntityDataComponents;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using Source.Scripts.VisitableComponents;
using UniRx;
using UnityEngine;

namespace Source.Scripts.States
{
    public class ProtectionState : State
    {
        [SerializeField, Range(0, 1)] private float _damageReduce;
        [SerializeField] private float _parryStartTime;
        [SerializeField] private float _parryEndTime;
        
        private Animator _animator;
        private static readonly int IsProtection = Animator.StringToHash("IsProtection");
        private float _previousReduce;
        private IDisposable _startParryDisposable;
        private IDisposable _endParryDisposable;
        
        private void OnEnable()
        {
            _animator ??= _entity.Get<AnimationHandler>().Animator;
            StopParry();
            _startParryDisposable?.Dispose();
            _endParryDisposable?.Dispose();

            _startParryDisposable = SetTimer(_parryStartTime, StartParry);
            _animator.SetBool(IsProtection,true);
        }

        private void OnDisable()
        {
            _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent = _previousReduce;
            _animator.SetBool(IsProtection,false);
            
            
            _startParryDisposable?.Dispose();
        }

        private void StartParry()
        {
            Debug.LogError("StartParry");
            
            EntityCurrentStatsData entityCurrentStatsData = _entity.AddOrGet<EntityCurrentStatsData>();
            _previousReduce = entityCurrentStatsData.DamageReducePercent;
            entityCurrentStatsData.DamageReducePercent = _damageReduce;

            _entity.Add(new ParryComponent() {WhoParryEntity = _entity});
            _endParryDisposable = SetTimer(_parryEndTime, StopParry);

        }

        private void StopParry()
        {
            _entity.Remove<ParryComponent>();

            Debug.LogError("StopParry");
        }
        private IDisposable SetTimer(float seconds, Action onComplete = null)
        {
            return Observable.Timer(TimeSpan.FromSeconds(seconds)).Subscribe(_=>onComplete?.Invoke());
        }
    }
}