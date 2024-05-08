using System;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    [Serializable]
    public class AttackStateComponentData
    {
        [field: SerializeField] public Transform WhoWasRotate { get; private set; }
        [field: SerializeField] public float RotationSpeed { get; private set; }

        public Transform Orientation { get; private set; }
        public Container<ICondition> ConditionsContainer { get; private set; }

        public void Initialize(Transform orientation, Container<ICondition> conditionsContainer)
        {
            Orientation = orientation;
            ConditionsContainer = conditionsContainer;
        }
    }
}