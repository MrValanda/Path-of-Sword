using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.MoveSetModule.Scripts;
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

        public MoveToAttackPointAction(IAttackPointCalculator attackPointCalculator,Entity senderEntity)
        {
            _attackPointCalculator = attackPointCalculator;
            _senderEntity = senderEntity;
        }

        public void OnStart()
        {
            _senderEntity.Get<Animation>().Animator.CrossFade("Movement", 0.1f, 0, 0);
        }

        public TaskStatus ExecuteAction()
        {
            Vector3 direction = _attackPointCalculator.GetDirection();
            _senderEntity.Get<IMovement>().Move(direction);
            _senderEntity.transform.forward = direction;
            
            return direction == Vector3.zero ? TaskStatus.Success : TaskStatus.Running;
        }

        public void OnExit()
        {
            _senderEntity.Get<IMovement>().Move(Vector3.zero);
        }
    }
}
