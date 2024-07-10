using System;
using System.Collections.Generic;
using System.Linq;
using Source.Modules.Tools;
using Source.Scripts.EntityLogic;
using States;
using UnityEngine;

namespace Source.Scripts
{
    public abstract class State : OptimizedMonoBehavior
    {
        public event Action<State> ExitState;
        public event Action<State> EnterState;
        public event Action<State> TransitionDetected;

        [SerializeField] private TransitionContainer _transitions;
        [SerializeField] private List<State> _subStates;
        
        [SerializeField] private float checkingTransitionDelay = 0.1f;
        
        [SerializeField] protected Entity _entity;
        
        private float _lastTick = 0.1f;
        
        private List<Transition> _detectedTransitions = new List<Transition>();
        protected bool IsPaused { get; private set; }

        public List<Transition> Transitions => _transitions?.GetTransitions() ?? new List<Transition>();

        protected override void OnValidate()
        {
            base.OnValidate();
            if (enabled)
            {
                enabled = false;
            }

            _lastTick = checkingTransitionDelay;
        }


        public void Enter()
        {
            enabled = true;
            _lastTick = checkingTransitionDelay;
            foreach (var transition in Transitions)
            {
                transition.NeedTransit += OnNeedTransit;
                transition.OnEnable();
            }

            foreach (var subState in _subStates)
            {
                subState.Enter();
            }

            EnterState?.Invoke(this);
            OnEnter();
        }

        public void Exit()
        {
            enabled = false;
            foreach (var transition in Transitions)
            {
                transition.NeedTransit -= OnNeedTransit;
                transition.OnDisable();
            }

            foreach (var subState in _subStates)
            {
                subState.Exit();
            }

            ExitState?.Invoke(this);
            OnExit();
        }

        private void OnNeedTransit(Transition obj)
        {
            _detectedTransitions.Add(obj);
            _lastTick = checkingTransitionDelay;
        }

        protected void Update()
        {
            _lastTick -= Time.deltaTime;
            if (_lastTick <= 0)
            {
                if (_detectedTransitions.Count > 0)
                {
                    var nextState = _detectedTransitions.OrderByDescending(x => x.Priority).FirstOrDefault()?.NextState;
                    if (nextState != null)
                    {
                        TransitionDetected?.Invoke(nextState);
                        _detectedTransitions.Clear();
                    }
                }

                _lastTick = checkingTransitionDelay;
            }

            OnUpdate();
        }

        protected virtual void OnUpdate()
        {
        }

        protected virtual void OnEnter()
        {
        }

        protected virtual void OnExit()
        {
        }

        public void SetPause(bool isPaused)
        {
            IsPaused = isPaused;
        }
    }
}