using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;
using Source.Scripts.BehaviorsNodes.SharedVariables;
using Source.Scripts.Enemy;
using Source.Scripts.GameActions;
using UnityEngine;

namespace Source.Scripts.BehaviorsNodes.Actions
{
    public class MoveToPoint : Action
    {
        public SharedTransform Point;
        public SharedVector3 RotationDirection;
        public SharedEnemyMovement SharedEnemyMovement;
        public SharedTransform SharedWhoMoved;
        public bool NeedRotate;
        private readonly CalculatorNavMeshPath _calculatorNavMeshPath = new CalculatorNavMeshPath();

        public override TaskStatus OnUpdate()
        {
            var sharedEnemyMovement = SharedEnemyMovement;
            Vector3 sourcePosition = SharedWhoMoved.Value.position;
            Vector3 targetPosition = Point.Value.position;
            Vector3 directionToNextPoint = SharedEnemyMovement.Value is EnemyMovement
                ? _calculatorNavMeshPath.GetDirectionToNextPoint(sourcePosition,
                    targetPosition)
                : targetPosition;
            if (Vector3.Distance(sourcePosition, targetPosition) <= 2f)
            {
                sharedEnemyMovement.Value.Move(Vector3.zero);
                if(NeedRotate)
                {
                    SharedWhoMoved.Value.forward = Point.Value.forward;
                }
                return TaskStatus.Success;
            }

            sharedEnemyMovement.Value.Move(directionToNextPoint);
            RotationDirection.Value = directionToNextPoint;
            return TaskStatus.Running;
        }
    }
}