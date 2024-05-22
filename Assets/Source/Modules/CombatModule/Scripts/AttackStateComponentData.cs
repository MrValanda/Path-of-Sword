using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Modules.CombatModule.Scripts
{
    public class AttackStateComponentData
    {
        public AttackStateComponentData(Transform whoWasRotate, float rotationDuration, Transform orientation, Container<IGameCondition> conditionsContainer)
        {
            WhoWasRotate = whoWasRotate;
            RotationDuration = rotationDuration;
            Orientation = orientation;
            ConditionsContainer = conditionsContainer;
        }

         public Transform WhoWasRotate { get; }
         public float RotationDuration { get; }
         public Transform Orientation { get; }
         public Container<IGameCondition> ConditionsContainer { get; }
    }
}