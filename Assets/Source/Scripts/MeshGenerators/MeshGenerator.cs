using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts
{
    public abstract class MeshGenerator : MonoBehaviour
    {
        public Material MeshMaterial { get; protected set; }
        public abstract void Init(IndicatorDataSetup indicatorDataSetup, LayerMask visionObstructingLayer);

    }
}