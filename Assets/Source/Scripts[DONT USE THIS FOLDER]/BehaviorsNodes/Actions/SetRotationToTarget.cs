using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Source.Scripts.BehaviorsNodes.Actions
{
    public class SetRotationToTarget : Action
    {
        public SharedVector3 RotationDirection;
        public SharedTransform Target;
        public SharedTransform SourceTransform;

        public override TaskStatus OnUpdate()
        {
            Vector3 targetPosition = Target.Value.position;
            targetPosition.y = 0;
            Vector3 sourcePosition = SourceTransform.Value.position;
            sourcePosition.y = 0;
           RotationDirection.Value = targetPosition - sourcePosition;
            return TaskStatus.Success;
        }
    }
}