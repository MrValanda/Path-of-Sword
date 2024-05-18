using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class MovementStateData
    {
        public MovementStateData(Transform whoMoved, Transform orientation, float rotationDuration, float acceleration)
        {
            WhoMoved = whoMoved;
            Orientation = orientation;
            RotationDuration = rotationDuration;
            Acceleration = acceleration;
        }

        public Transform WhoMoved { get; }
        public Transform Orientation { get; }
        public float RotationDuration{get; }
        public float Acceleration{get; }
        
    }
}