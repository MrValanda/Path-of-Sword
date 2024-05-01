using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Source.Scripts.BehaviorsNodes.Actions
{
    public class RotateByDirection : Action
    {
        public SharedVector3 RotationDirection;
        public SharedTransform WhoWasRotate;
        public float MaxRadiansDeltaAngle = 10f;
        public SharedBool BlockRotation;

        public override TaskStatus OnUpdate()
        {
            if (BlockRotation.Value) return TaskStatus.Success;

            Vector3 currentDirection = WhoWasRotate.Value.transform.forward;
            Vector3 desiredDirection = RotationDirection.Value.normalized;

            float rotationSpeed = MaxRadiansDeltaAngle * Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(currentDirection, desiredDirection, rotationSpeed, 0f);
            WhoWasRotate.Value.transform.rotation = Quaternion.LookRotation(newDirection);
            if (Vector3.Dot(desiredDirection, WhoWasRotate.Value.transform.forward) < 0.9f) return TaskStatus.Running;
            WhoWasRotate.Value.transform.rotation = Quaternion.LookRotation(desiredDirection);
            return TaskStatus.Success;
        }
    }
}