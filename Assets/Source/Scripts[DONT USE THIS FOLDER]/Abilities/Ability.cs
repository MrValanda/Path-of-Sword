using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.CodeLibrary.ServiceBootstrap;
using Source.Scripts.AnimationEventListeners;
using Source.Scripts.AnimationEventNames;
using Source.Scripts.Factories;
using Source.Scripts.Interfaces;
using Source.Scripts.Setups;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Source.Scripts.Abilities
{
    public class Ability : IDisposable
    {
        private readonly string _stateName;

        private readonly AbilityEventListener _abilityEventListener;
        private readonly Animator _animator;
        private readonly IndicatorHandler.IndicatorHandler _indicatorHandlerPrefab;
        private readonly BaseAbilitySetup _abilitySetup;

        private readonly List<IAbilityAction> _abilityStartedActions;
        private readonly List<IAbilityAction> _abilityEndedActions;

        public BaseAbilitySetup AbilitySetup => _abilitySetup;

        private AnimationPartPlayer _animationPartPlayer;
        private Transform _castPoint;
        private Enemy.Enemy _abilityCaster;
        private IndicatorHandler.IndicatorHandler _spawnedIndicatorHandler;
        private IDisposable _indicatorTimer;
        private IndicatorHandlerFactory _indicatorHandlerFactory;

        public bool IsCasted { get; private set; }

        public Ability(AbilityDataContainer abilityDataContainer, string stateName = "Ability")
        {
            _stateName = stateName;
            _abilityEventListener = abilityDataContainer.AbilityEventListener;
            _animator = abilityDataContainer.Animator;
            _abilitySetup = abilityDataContainer.AbilitySetup;
            _abilityStartedActions = _abilitySetup?.AbilityStartedActions?.AbilityActions ?? new List<IAbilityAction>();
            _abilityEndedActions = _abilitySetup?.AbilityEndedActions?.AbilityActions ?? new List<IAbilityAction>();
            _abilityEventListener.AbilityStarted += OnAbilityStarted;
            _abilityEventListener.AbilityEnded += OnAbilityEnded;
            _abilityEventListener.PreparationStarted += OnPreparationStarted;
            _abilityEventListener.PreparationEnded += OnPreparationEnded;

            ServiceLocator.Global.Get(out _indicatorHandlerFactory);
             _indicatorHandlerPrefab = _indicatorHandlerFactory.GetFactoryItem(_abilitySetup.AbilityDataSetup.IndicatorDataSetup.GetType());
        }

        ~Ability()
        {
            Dispose();
        }

        private void OnPreparationStarted()
        {
            AnimationEvent startPreparationEvent =
                _abilitySetup.AbilityAnimation.events.FirstOrDefault(x =>
                    x.functionName.Equals(AbilityEventNames.PreparationStartEventName));

            AnimationEvent endPreparationEvent =
                _abilitySetup.AbilityAnimation.events.FirstOrDefault(x =>
                    x.functionName.Equals(AbilityEventNames.PreparationEndEventName));

            if (startPreparationEvent == null || endPreparationEvent == null)
                return;

            float preparationTime = endPreparationEvent.time - startPreparationEvent.time;
            _animationPartPlayer.AnimatePartAnimation(_abilitySetup.AbilityDataSetup.PreparationAttackTime,
                preparationTime);

            if (_abilitySetup.AbilityDataSetup.NeedPreparationIndicator)
            {
                var position = _castPoint.position;
                _spawnedIndicatorHandler = Object.Instantiate(_indicatorHandlerPrefab,
                    position, Quaternion.identity, _abilityCaster.transform);

                Transform circleTransform = _spawnedIndicatorHandler.transform;
                Vector3 transformPosition = position;
                transformPosition.y = _abilityCaster.transform.position.y;
                circleTransform.position = transformPosition;
                circleTransform.forward = _castPoint.forward;

                _spawnedIndicatorHandler.Init(_abilitySetup.AbilityDataSetup.IndicatorDataSetup,
                    _abilitySetup.AbilityDataSetup.ObstacleLayers);

                _indicatorTimer?.Dispose();
                float time = 0;
                _indicatorTimer = Observable.EveryUpdate()
                    .TakeWhile(_ => time < _abilitySetup.AbilityDataSetup.PreparationAttackTime).Subscribe(_ =>
                    {
                        time += Time.deltaTime;
                        _spawnedIndicatorHandler.SetDuration(time / _abilitySetup.AbilityDataSetup
                            .PreparationAttackTime);
                    }, () =>
                    {
                        {
                            if (_spawnedIndicatorHandler == null) return;
                            Object.Destroy(_spawnedIndicatorHandler.gameObject);
                        }
                    });
            }
        }

        private void OnPreparationEnded()
        {
            _animationPartPlayer.Reset();
        }

        public void Dispose()
        {
            _abilityEventListener.PreparationStarted -= OnPreparationStarted;
            _abilityEventListener.PreparationEnded -= OnPreparationEnded;
            _abilityEventListener.AbilityStarted -= OnAbilityStarted;
            _abilityEventListener.AbilityEnded -= OnAbilityEnded;
            IsCasted = false;
            if (_spawnedIndicatorHandler == null) return;
            Object.Destroy(_spawnedIndicatorHandler.gameObject);
        }

        public float GetAbilityTime()
        {
            return _abilitySetup.AbilityAnimation.length + _abilitySetup.AbilityDataSetup.PreparationAttackTime +
                   _abilitySetup.AbilityDataSetup.StartAttackTime;
        }

        [Button]
        public void CastSpell(Transform castPoint, Enemy.Enemy abilityCaster)
        {
            _abilityCaster = abilityCaster;
            _castPoint = castPoint;

            abilityCaster.EnemyComponents.Animation.OverrideAnimation(_stateName, _abilitySetup.AbilityAnimation);
            _animationPartPlayer ??= new AnimationPartPlayer(_animator);

            PlayCastAnimation();
            IsCasted = true;
            abilityCaster.ComponentContainer.GetComponent<IDying>().Dead -= OnDied;
            abilityCaster.ComponentContainer.GetComponent<IDying>().Dead += OnDied;
        }

        private void OnDied(IDying obj)
        {
            obj.Dead -= OnDied;
            StopCast();
        }

        public void StopCast()
        {
            Dispose();
            _animationPartPlayer.Reset();
            _indicatorTimer?.Dispose();
        }

        private void OnAbilityStarted()
        {
            foreach (IAbilityAction abilityStartedAction in _abilityStartedActions)
            {
                abilityStartedAction.ExecuteAction(_castPoint, _abilityCaster, _abilitySetup.AbilityDataSetup);
            }
        }

        private void OnAbilityEnded()
        {
            Dispose();
            _animationPartPlayer.Reset();
            foreach (IAbilityAction abilityEndedAction in _abilityEndedActions)
            {
                abilityEndedAction.ExecuteAction(_castPoint, _abilityCaster, _abilitySetup.AbilityDataSetup);
            }
        }

        private void PlayCastAnimation()
        {
            AnimationEvent startPreparationEvent =
                _abilitySetup.AbilityAnimation.events.FirstOrDefault(x =>
                    x.functionName.Equals(AbilityEventNames.PreparationStartEventName));

            if (startPreparationEvent == null) return;

            _animationPartPlayer.AnimatePartAnimation(_abilitySetup.AbilityDataSetup.StartAttackTime,
                startPreparationEvent.time);
            _animator.CrossFadeInFixedTime(_stateName, 0.2f, 0, 0);
        }
    }
}