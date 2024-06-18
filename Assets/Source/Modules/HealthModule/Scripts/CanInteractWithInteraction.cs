using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Sirenix.Serialization;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.InteractionModule.Scripts;
using Source.Scripts.EntityLogic;
using UniRx;
using UnityEngine;

namespace Source.Modules.HealthModule.Scripts
{
    [Serializable]
    public class CanInteractWithInteraction : Transition
    {
        [SerializeField] private Entity _entity;
        [OdinSerialize] private List<IGameCondition> _conditions;
        private IDisposable _disposable;

        public override void OnEnable()
        {
            _conditions.ForEach(x => x.InitData());
            _disposable = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    InteractionMono selectedInteraction = _entity.Get<InteractionSelector>().SelectedInteraction;

                    if (selectedInteraction != null && selectedInteraction.CanInteract(_entity) &&
                        _conditions.TrueForAll(a => a.GetConditionStatus() == TaskStatus.Success))
                    {
                        Debug.LogError("Transittt");
                        OnNeedTransit(this);
                        _disposable?.Dispose();
                    }
              
                });
        }

        public override void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}