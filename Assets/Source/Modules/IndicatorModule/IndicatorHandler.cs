using Sirenix.OdinInspector;
using Source.Scripts.Setups;
using UnityEngine;

namespace Source.Scripts.IndicatorHandler
{
    public class IndicatorHandler : MonoBehaviour
    {
        [SerializeField] private MeshGenerator _visionCone;
        private static readonly int Duration = Shader.PropertyToID("_Duration");

        public void Init(IndicatorDataSetup indicatorDataSetup, LayerMask obstacles)
        {
            _visionCone.Init(indicatorDataSetup, obstacles);
            SetDuration(0);
        }

        [Button]
        public void SetDuration(float duration)
        {
            duration = Mathf.Clamp(duration, 0, 1);
            _visionCone.MeshMaterial.SetFloat(Duration, duration);
        }
    }
}