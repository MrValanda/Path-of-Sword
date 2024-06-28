using UnityEngine;

namespace Source.Scripts.Setups
{
    public class RectangleIndicatorDataSetup : IndicatorDataSetup
    {
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public float Width { get; private set; }
    }
}