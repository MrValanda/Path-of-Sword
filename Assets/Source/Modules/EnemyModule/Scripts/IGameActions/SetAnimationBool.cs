using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class SetAnimationBool : IGameAction
    {
        private readonly bool _desiredBool;
        private readonly UnityEngine.Animator _animator;
        private readonly string _boolName;

        public SetAnimationBool( UnityEngine.Animator animator,bool desiredBool, string boolName)
        {
            _desiredBool = desiredBool;
            _animator = animator;
            _boolName = boolName;
        }

        public void OnExit()
        {
            _animator.SetBool(_boolName,!_desiredBool);
        }

        public TaskStatus ExecuteAction()
        {
            _animator.SetBool(_boolName,_desiredBool);
            return TaskStatus.Success;
        }
    }
}