using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.Abilities;
using Source.Scripts.Enemy;
using Source.Scripts.EntityLogic;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Abilities;

namespace Source.Modules.EnemyModule.Scripts.IGameConditions
{
    public class CanUseAbility : IGameCondition
    {
        private readonly AbilityCaster _abilityCaster;
        private readonly Entity _entity;

        public CanUseAbility(AbilityCaster abilityCaster, Entity entity)
        {
            _abilityCaster = abilityCaster;
            _entity = entity;
        }

        public TaskStatus GetConditionStatus()
        {
            Ability currentAbility = _entity.AddOrGet<AbilityUseData>().CurrentAbility;
            bool abilityCasted = currentAbility?.IsCasted ?? false;

            return _abilityCaster.CanUseAbility() && abilityCasted == false
                ? TaskStatus.Success
                : TaskStatus.Failure;
        }
    }
}