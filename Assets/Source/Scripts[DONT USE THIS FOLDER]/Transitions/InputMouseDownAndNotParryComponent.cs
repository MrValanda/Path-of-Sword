using System;
using Source.Modules.CombatModule.Scripts;
using Source.Scripts.EntityLogic;
using UniRx;
using UnityEngine;

namespace Source.Scripts.Transitions
{
    [Serializable]
    public class InputMouseDownAndNotParryComponent : Transition
    {
        [SerializeField] private int _mouseKey;
        [SerializeField] private Entity _entity;

        private IDisposable _disposable;

        public override void OnEnable()
        {
            _disposable = Observable.EveryUpdate()
                .Where(x => Input.GetMouseButtonDown(_mouseKey) && _entity.Contains<ParryComponent>() == false).Take(1)
                .Subscribe(_ => { OnNeedTransit(this); });
        }

        public override void OnDisable()
        {
            _disposable?.Dispose();
        }
    }
}