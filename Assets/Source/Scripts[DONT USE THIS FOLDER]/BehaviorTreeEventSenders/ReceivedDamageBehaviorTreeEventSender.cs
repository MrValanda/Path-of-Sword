using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts.BehaviorTreeEventSenders
{
    public class ReceivedDamageBehaviorTreeEventSender : SerializedMonoBehaviour
    {
        private const string ReceivedDamageEventName = "ReceivedDamage";

        [SerializeField] private BehaviorTree _behaviorTree;
        [OdinSerialize] private IDamageable _eventSender;

        public void OnEnable()
        { 
            _eventSender.ReceivedDamage += OnReceivedDamage;
        }

        public void OnDisable()
        {
            _eventSender.ReceivedDamage -= OnReceivedDamage;
        }

        private void OnReceivedDamage(double damage)
        {
            _behaviorTree.SendEvent<object>(ReceivedDamageEventName, damage);
        }
    }
}