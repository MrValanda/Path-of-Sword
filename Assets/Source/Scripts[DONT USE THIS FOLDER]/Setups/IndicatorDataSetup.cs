using System;
using UnityEngine;

namespace Source.Scripts.Setups
{
    [Serializable]
    public abstract class IndicatorDataSetup
    {
        [field: SerializeField] public Texture MainTexture { get; private set; }
        [field: SerializeField] public Texture SecondTexture { get; private set; }
        [field: SerializeField] public Color BorderColor { get; private set; } = Color.red;
        [field: SerializeField] public Color StartColor { get; private set; } = Color.red;
        [field: SerializeField] public Color EndColor { get; private set; } = Color.red;
    }
}