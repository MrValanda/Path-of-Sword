using System;
using UnityEngine;

namespace Source.Scripts.Setups
{
    [Serializable]
    public class ConeIndicatorDataSetup : IndicatorDataSetup
    {
        [field: SerializeField] public float Angle { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
    }
}