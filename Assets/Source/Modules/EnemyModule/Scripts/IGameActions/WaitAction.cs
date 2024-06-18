using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class WaitAction : IGameAction
    {
        private readonly float _waitTime;
        private float _lastTime;
        
        public WaitAction(float waitTime)
        {
            _waitTime = waitTime;
        }

        public void OnStart()
        {
            _lastTime = Time.time;
        }

        public TaskStatus ExecuteAction()
        {
            if (_waitTime < 0) return TaskStatus.Running;
            
            return Time.time - _lastTime >= _waitTime ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}