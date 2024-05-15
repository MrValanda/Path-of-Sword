using Source.Modules.CombatModule.Scripts;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Tools;
using UnityEngine;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.States
{
    public class ProtectionState : State
    {
        private static readonly int IsProtection = Animator.StringToHash("IsProtection");
        private static readonly int Protect = Animator.StringToHash("Protect");
        
        [SerializeField, Range(0, 1)] private float _damageReduce;

        public float PreviousDamageReduce { get; private set; }
        
        private Animator _animator;
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
            _animator.SetBool(IsProtection,false);
        }
        
        private void StartParry()
        {
            _entity.Add(new ParryComponent() {WhoParryEntity = _entity});
        }

        private void ProtectionStart()
        {
            EntityCurrentStatsData entityCurrentStatsData = _entity.AddOrGet<EntityCurrentStatsData>();
            PreviousDamageReduce = entityCurrentStatsData.DamageReducePercent;
            entityCurrentStatsData.DamageReducePercent = _damageReduce;
        }
        
        private void StopParry()
        {
            _entity.Remove<ParryComponent>();
        }
    }
}