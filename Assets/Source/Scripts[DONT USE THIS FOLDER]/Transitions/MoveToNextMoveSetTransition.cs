using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Sirenix.Serialization;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.CombatModule.Scripts;
using UniRx;
using UnityEngine;

namespace Transitions
{
    [Serializable]
    public class MoveToNextMoveSetTransition : Transition
    {
        [SerializeField] private AttackEventListener _attackEventListener;
        [OdinSerialize] private List<IGameCondition> _conditionsToMoveNext;

        private IDisposable _disposable;
        public override void OnEnable()
        {
            _attackEventListener.StartListenCombo += OnStartListenCombo;
            _attackEventListener.StopListenCombo += OnStopListenCombo;
            _conditionsToMoveNext.ForEach(x => x.InitData());
        }

        public override void OnDisable()
        {
            _attackEventListener.StartListenCombo -= OnStartListenCombo;
            _attackEventListener.StopListenCombo -= OnStopListenCombo;
            _disposable?.Dispose();   
        }

        private void OnStartListenCombo()
        {
            _disposable = Observable.EveryUpdate()
                .Where(_ => _conditionsToMoveNext.TrueForAll(x => x.GetConditionStatus() == TaskStatus.Success))
                .Take(1)
                .Subscribe(_ => { OnNeedTransit(this); });
        }

        private void OnStopListenCombo()
        {
            _disposable?.Dispose();   
        }
    }
}