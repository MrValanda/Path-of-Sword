using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class DelayedExecutorAction : IGameAction
    {
        private readonly float _delay;
        private float _lastExecutedTime;
        private readonly List<IGameAction> _executionGameActions;

        public DelayedExecutorAction(float delay, List<IGameAction> executionGameActions)
        {
            _delay = delay;
            _executionGameActions = executionGameActions;
        }

        public void OnStart()
        {
            _executionGameActions.ForEach(x => x.OnStart());
        }

        public void OnExit()
        {
            _executionGameActions.ForEach(x => x.OnExit());
        }

        public TaskStatus ExecuteAction()
        {
            if (Time.time - _lastExecutedTime <= _delay)
                return TaskStatus.Success;
            
            _lastExecutedTime = Time.time;
            _executionGameActions.ForEach(x => x.ExecuteAction());
            return TaskStatus.Success;
        }
    }
}