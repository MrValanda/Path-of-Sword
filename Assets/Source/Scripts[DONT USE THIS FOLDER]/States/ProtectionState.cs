using Source.Modules.CombatModule.Scripts;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using Source.Scripts.VisitableComponents;
using UnityEngine;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.States
{
    public class ProtectionState : State
    {
        private static readonly int IsProtection = Animator.StringToHash("IsProtection");
        private static readonly int Protect = Animator.StringToHash("Protect");
    
        [SerializeField, Range(0, 1)] private float _damageReduce;

        private Animator _animator;
  
        private float _previousReduce;
        private ProtectionEventListener _protectionEventListener;
        
        private void OnEnable()
        {
            _animator ??= _entity.Get<AnimationHandler>().Animator;
            _protectionEventListener ??= _entity.Get<ProtectionEventListener>();
            
            if (_entity.TryGet(out ParryHandler parryHandler))
            {
                _protectionEventListener.ParryStarted -= StartParry;
                _protectionEventListener.ParryEnded -= StopParry;
                _protectionEventListener.ProtectionStarted -= ProtectionStart;

                _protectionEventListener.ParryStarted += StartParry;
                _protectionEventListener.ParryEnded += StopParry;
                _protectionEventListener.ProtectionStarted += ProtectionStart;
                
                parryHandler.Parry();
            }

            _animator.SetBool(IsProtection,true);
            _animator.SetTrigger(Protect);
        }

        private void OnDisable()
        {
            _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent = _previousReduce;
            _animator.SetBool(IsProtection,false);
        }

        private void StartParry()
        {
            _entity.Add(new ParryComponent() {WhoParryEntity = _entity});
        }

        private void ProtectionStart()
        {
            EntityCurrentStatsData entityCurrentStatsData = _entity.AddOrGet<EntityCurrentStatsData>();
            _previousReduce = entityCurrentStatsData.DamageReducePercent;
            entityCurrentStatsData.DamageReducePercent = _damageReduce;
        }
        
        private void StopParry()
        {
            _entity.Remove<ParryComponent>();
        }
    }
}