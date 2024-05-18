using UnityEngine;

namespace Source.Modules.MovementModule.Scripts
{
    public class DodgeStateData
    {
        public DodgeStateData(Transform orientation, Transform whoWasRotate)
        {
            Orientation = orientation;
            WhoWasRotate = whoWasRotate;
        }

        public Transform Orientation { get; }
        public Transform WhoWasRotate { get; }
    }
}