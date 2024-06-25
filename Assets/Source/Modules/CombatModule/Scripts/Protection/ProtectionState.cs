using System.Collections.Generic;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Modules.AudioModule;
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
        [SerializeField] private List<State> _subStatesAfterParry = new List<State>();

        private Animator _animator;
        private ProtectionEventListener _protectionEventListener;
        private bool _needEnterToSubStates;

        private void OnEnable()
        {
            _needEnterToSubStates = true;
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

            _animator.SetBool(IsProtection, true);
            _animator.SetTrigger(Protect);
        }

        private void OnDisable()
        {
            _animator.SetBool(IsProtection, false);
            _needEnterToSubStates = false;
            foreach (State state in _subStatesAfterParry)
            {
                if (state.enabled)
                {
                    state.Exit();
                }
            }
        }

        private void StartParry()
        {
            _entity.Add(new ParryComponent() {WhoParryEntity = _entity});
        }

        private void ProtectionStart()
        {
            _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent = _damageReduce;
            ServiceLocator.For(this).Get<SoundPlayer>().PlaySoundByType(SoundType.Sword_Protection_Start_0);
        }

        private void StopParry()
        {
            if(_needEnterToSubStates)
            {
                foreach (State state in _subStatesAfterParry)
                {
                    if (state.enabled == false)
                    {
                        state.Enter();
                    }
                }
            }

            _entity.Remove<ParryComponent>();
        }
    }
}