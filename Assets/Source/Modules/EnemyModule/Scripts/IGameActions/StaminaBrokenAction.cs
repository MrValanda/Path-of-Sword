using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.StaminaModule.Scripts;
using Source.Scripts.Enemy;
using Source.Scripts.EntityLogic;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class StaminaBrokenAction : IGameAction
    {
        private static readonly int ParryBroken = UnityEngine.Animator.StringToHash("ParryBroken");
        private static readonly int IsStunned = UnityEngine.Animator.StringToHash("IsStunned");
        private readonly Entity _entity;

        public StaminaBrokenAction(Entity entity)
        {
            _entity = entity;
        }

        public void OnExit()
        {
            _entity.Get<StaminaController>().ResetStamina();
            _entity.Get<Animation>().Animator.SetBool(IsStunned,false);
        }

        public TaskStatus ExecuteAction()
        {
            _entity.Get<Animation>().Animator.SetTrigger(ParryBroken);
            _entity.Get<Animation>().Animator.SetBool(IsStunned,true);
            return TaskStatus.Success;
        }
    }
}