using BehaviorDesigner.Runtime;
using Source.Scripts.Abilities;
using UnityEngine;
using VisitableComponents;

namespace Source.Scripts.BehaviorTreeEventSenders
{
    public class NeedStayAfkBehaviorTreeEventSender : MonoBehaviour
    {
        private const string NeedStayAfkEventName = "NeedStayAfk";
        [SerializeField] private BehaviorTree _behaviorTree;
        [SerializeField] private AbilityCaster _abilityCaster;

        public void OnEnable()
        {
            _abilityCaster.AbilityEnded += OnAbilityEnded;
        }

        public void OnDisable()
        {
            _abilityCaster.AbilityEnded -= OnAbilityEnded;
        }

        public void SendEvent(float afkTime)
        {
            if (afkTime == 0) return;
            _behaviorTree.SendEvent<object>(NeedStayAfkEventName, afkTime);
        }

        private void OnAbilityEnded(Ability obj)
        {
            if (obj == null)
            {
                SendEvent(0);
                return;
            }
            SendEvent(obj.AbilitySetup.AbilityDataSetup.AfkTimeAfterAbility);
        }
    }
}