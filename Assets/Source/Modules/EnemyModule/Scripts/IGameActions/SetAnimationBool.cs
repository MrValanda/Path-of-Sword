using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using UnityEngine;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class SetAnimationBool : IGameAction
    {
        private readonly bool _desiredBool;
        private readonly Animator _animator;
        private readonly string _boolName;
        private readonly bool _needResetBool;

        public SetAnimationBool(Animator animator, bool desiredBool, string boolName, bool needResetBool = true)
        {
            _desiredBool = desiredBool;
            _animator = animator;
            _boolName = boolName;
            _needResetBool = needResetBool;
        }

        public void OnExit()
        {
            if (_needResetBool == false)return;

            _animator.SetBool(_boolName, !_desiredBool);
        }

        public TaskStatus ExecuteAction()
        {
            _animator.SetBool(_boolName, _desiredBool);
            return TaskStatus.Success;
        }
    }
}