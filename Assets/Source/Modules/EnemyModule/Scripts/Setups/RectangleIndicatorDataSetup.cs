using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Source.Scripts.Setups
{
    [Serializable]
    public class RectangleIndicatorDataSetup : IndicatorDataSetup
    {
        [field: SerializeField] public float MaxDistance { get; private set; }
        [field: SerializeField] public float Width { get; private set; }
    }
}