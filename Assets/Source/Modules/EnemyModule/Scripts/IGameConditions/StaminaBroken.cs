using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Modules.StaminaModule.Scripts;

namespace Source.Modules.EnemyModule.Scripts.IGameConditions
{
    public class StaminaBroken : IGameCondition
    {
        private readonly StaminaModel _staminaModel;

        public StaminaBroken(StaminaModel staminaModel)
        {
            _staminaModel = staminaModel;
        }

        public TaskStatus GetConditionStatus()
        {
            return _staminaModel.CurrentStamina <= 0 ? TaskStatus.Success : TaskStatus.Failure;
        }
    }
}