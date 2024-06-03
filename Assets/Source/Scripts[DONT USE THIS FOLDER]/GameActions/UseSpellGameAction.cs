using System;
using BehaviorDesigner.Runtime.Tasks;
using Source.Modules.BehaviorTreeModule;
using Source.Scripts.Abilities;
using Source.Scripts.Interfaces;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Abilities;
using UnityEngine;

namespace Source.Scripts.GameActions
{
    [Serializable]
    public class UseSpellGameAction : IGameAction
    {
        [SerializeField] private AbilityCaster _abilityCaster;

        public UseSpellGameAction(AbilityCaster abilityCaster)
        {
            _abilityCaster = abilityCaster;
        }

        public TaskStatus ExecuteAction()
        {
            if (_abilityCaster.IsAbilityProcessed == false)
            {
                _abilityCaster.UseSpell();
            }

            return TaskStatus.Success;
        }
    }
}