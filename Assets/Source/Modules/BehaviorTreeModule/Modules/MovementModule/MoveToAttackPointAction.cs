using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.MovementModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Scripting;
using Animation = Source.Scripts.Enemy.Animation;

namespace Source.Modules.BehaviorTreeModule.Modules.MovementModule
{
    [Preserve]
    public class MoveToAttackPointAction : IGameAction
    {
        private readonly IAttackPointCalculator _attackPointCalculator;
        private readonly Entity _senderEntity;
        private static readonly int IsMovement = Animator.StringToHash("IsMovement");

        public MoveToAttackPointAction(IAttackPointCalculator attackPointCalculator, Entity senderEntity)
        {
            _attackPointCalculator = attackPointCalculator;
            _senderEntity = senderEntity;
        }

        public void OnStart()
        {
            _senderEntity.Add(new MoveToTargetComponent());
            _senderEntity.Get<Animation>().Animator.SetBool(IsMovement, true);
        }

        public void OnExit()
        {
            _senderEntity.Remove<MoveToTargetComponent>();
            _senderEntity.Get<Animation>().Animator.SetBool(IsMovement, false);
            _senderEntity.Get<IMovement>().Move(Vector3.zero);
        }

        public TaskStatus ExecuteAction()
        {
            Vector3 direction = _attackPointCalculator.GetDirection();
            _senderEntity.transform.forward = direction;
            _senderEntity.Get<IMovement>().Move(direction);
            return direction == Vector3.zero ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}