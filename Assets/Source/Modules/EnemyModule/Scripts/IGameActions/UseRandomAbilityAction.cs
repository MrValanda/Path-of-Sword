using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.Abilities;
using Source.Scripts.BehaviorTreeEventSenders;
using Source.Scripts.Enemy;
using Source.Scripts.EntityLogic;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Abilities;
using Transform = UnityEngine.Transform;

namespace Source.Modules.EnemyModule.Scripts.IGameActions
{
    public class UseRandomAbilityAction : IGameAction
    {
        private readonly AbilityCaster _abilityCaster;
        private readonly Entity _entity;
        private bool _needCastAbility;
        private static readonly int IsAbilityCast = UnityEngine.Animator.StringToHash("IsAbilityCast");

        public UseRandomAbilityAction(AbilityCaster abilityCaster, Entity entity)
        {
            _abilityCaster = abilityCaster;
            _entity = entity;
        }

        public void OnStart()
        {
            _entity.Get<Animation>().Animator.SetBool(IsAbilityCast, true);
            _needCastAbility = true;
            _abilityCaster.AbilityEnded += OnAbilityEnded;
        }

        public void OnExit()
        {
            _abilityCaster.AbilityEnded -= OnAbilityEnded;
            _entity.Get<Animation>().Animator.SetBool(IsAbilityCast, false);
            _needCastAbility = false;
            _abilityCaster.StopCastSpell();
        }

        private void OnAbilityEnded(Ability obj)
        {
            float afkTimeAfterAbility = obj.AbilitySetup.AbilityDataSetup.AfkTimeAfterAbility;
            if (afkTimeAfterAbility == 0)
            {
                return;
            }

            _entity.Get<NeedStayAfkBehaviorTreeEventSender>()
                .SendEvent(afkTimeAfterAbility);
        }

        public TaskStatus ExecuteAction()
        {
            if (_needCastAbility)
            {
                _needCastAbility = false;
                _abilityCaster.UseSpell();
            }

            return _abilityCaster.IsAbilityProcessed ? TaskStatus.Running : TaskStatus.Success;
        }
    }
}