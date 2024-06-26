using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class SetRandomAnimationFloatValue : IGameAction
    {
        private readonly Animator _sender;
        private readonly string _animationFloatName;
        private readonly float _minRange;
        private readonly float _maxRange;
        
        public SetRandomAnimationFloatValue(Animator sender, string animationFloatName, float minRange, float maxRange)
        {
            _sender = sender;
            _animationFloatName = animationFloatName;
            _minRange = minRange;
            _maxRange = maxRange;
        }

        public TaskStatus ExecuteAction()
        {
            _sender.SetFloat(_animationFloatName, Random.Range(_minRange, _maxRange));
            return TaskStatus.Success;
        }
    }
}