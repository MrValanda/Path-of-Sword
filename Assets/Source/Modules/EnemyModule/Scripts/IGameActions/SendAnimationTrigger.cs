using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class SendAnimationTrigger : IGameAction
    {
        private readonly Animator _animator;
        private readonly string _triggerName;

        public SendAnimationTrigger(Animator animator, string triggerName)
        {
            _animator = animator;
            _triggerName = triggerName;
        }

        public TaskStatus ExecuteAction()
        {
            _animator.SetTrigger(_triggerName);
            return TaskStatus.Success;
        }
    }
}