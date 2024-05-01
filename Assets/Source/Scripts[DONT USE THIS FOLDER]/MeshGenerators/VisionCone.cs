using System.Collections.Generic;
using Sirenix.OdinInspector;
using Source.Scripts.Setups;
using UnityEngine;
using UnityEngine.Rendering;

namespace Source.Scripts
{
    public class VisionCone : MeshGenerator
    {
        [SerializeField] private Material _visionConeMaterial;
        [SerializeField] private float _visionRange;
        [SerializeField] private float _visionAngle;

        [SerializeField] private LayerMask _visionObstructingLayer;
        [SerializeField] private int _visionConeResolution = 120;

        private Mesh _visionConeMesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private Transform _selfTransform;

        private Vector3[] _vertices;

        private int[] _triangles;
        private float _currentAngle;
        private float _angleInc;
        private float _defaultVisionAngle;
        private bool _initialized;
        private readonly List<VisualConeRaycastData> _visualConeRaycastData = new List<VisualConeRaycastData>();

#if UNITY_EDITOR
        [Button]
        private void DebugDraw()
        {
            if (Application.isPlaying)
                return;

            RecalculateValues();
            DrawVisionCone();
        }
#endif

        public override void Init(IndicatorDataSetup indicatorDataSetup, LayerMask visionObstructingLayer)
        {
            if (indicatorDataSetup is not ConeIndicatorDataSetup coneIndicatorDataSetup)
            {
                Debug.LogError($"{indicatorDataSetup} is not {nameof(RectangleIndicatorDataSetup)}");
                return;
            }

            _initialized = true;
            _visionRange = coneIndicatorDataSetup.Radius;
            _visionAngle = coneIndicatorDataSetup.Angle;
            _visionObstructingLayer = visionObstructingLayer;
            MeshMaterial ??= Instantiate(_visionConeMaterial);
            MeshMaterial.SetColor("_StartColor", indicatorDataSetup.StartColor);
            MeshMaterial.SetColor("_EndColor", indicatorDataSetup.EndColor);
            MeshMaterial.SetColor("_BorderColor", indicatorDataSetup.BorderColor);
            MeshMaterial.SetTexture("_MainTex", indicatorDataSetup.MainTexture);
            MeshMaterial.SetTexture("_SecondTex", indicatorDataSetup.SecondTexture);
            RecalculateValues();
        }

        private void Start()
        {
            RecalculateValues();
        }

        private void RecalculateValues()
        {
            _selfTransform = transform;

            if (_meshRenderer == null)
            {
                _meshRenderer = _selfTransform.TryGetComponent(out MeshRenderer meshRenderer)
                    ? meshRenderer
                    : _selfTransform.gameObject.AddComponent<MeshRenderer>();
                _meshRenderer.shadowCastingMode = ShadowCastingMode.Off;
            }

            _meshRenderer.material = MeshMaterial ? MeshMaterial : _visionConeMaterial;

            if (_meshFilter == null)
                _meshFilter = _selfTransform.TryGetComponent(out MeshFilter meshFilter)
                    ? meshFilter
                    : _selfTransform.gameObject.AddComponent<MeshFilter>();

            _visionConeMesh = new Mesh();
            _defaultVisionAngle = _visionAngle * Mathf.Deg2Rad;
            _currentAngle = -_defaultVisionAngle / 2;
            _angleInc = _defaultVisionAngle / (_visionConeResolution - 1);

            _triangles = new int[(_visionConeResolution - 1) * 3];
            _vertices = new Vector3[_visionConeResolution + 1];

            _vertices[0] = Vector3.zero;
            _visualConeRaycastData.Clear();

            for (int i = 0; i < _visionConeResolution; i++)
            {
                _visualConeRaycastData.Add(new VisualConeRaycastData()
                {
                    Sin = Mathf.Sin(_currentAngle),
                    Cos = Mathf.Cos(_currentAngle)
                });

                _currentAngle += _angleInc;
            }

            for (int i = 0, j = 0; i < _triangles.Length; i += 3, j++)
            {
                _triangles[i] = 0;
                _triangles[i + 1] = j + 1;
                _triangles[i + 2] = j + 2;
            }
        }

        private void Update()
        {
            if (_initialized == false) return;
            DrawVisionCone();
        }


        private void DrawVisionCone()
        {
            _currentAngle = -_defaultVisionAngle * 0.5f;
            _vertices[0] = Vector3.zero;

            for (int i = 0; i < _visionConeResolution; i++)
            {
                Vector3 raycastDirection = (_selfTransform.forward * _visualConeRaycastData[i].Cos) +
                                           (_selfTransform.right * _visualConeRaycastData[i].Sin);
                Vector3 forward = (Vector3.forward * _visualConeRaycastData[i].Cos) +
                                  (Vector3.right * _visualConeRaycastData[i].Sin);

                if (Physics.Raycast(transform.position, raycastDirection, out RaycastHit hit, _visionRange,
                    _visionObstructingLayer))
                    _vertices[i + 1] = forward * hit.distance;
                else
                    _vertices[i + 1] = forward * _visionRange;

                _currentAngle += _angleInc;
            }

            _visionConeMesh.Clear();
            _visionConeMesh.vertices = _vertices;
            _visionConeMesh.triangles = _triangles;

            Vector2[] uv = new Vector2[_visionConeResolution + 1];
            uv[0] = Vector2.one * 0.5f;

            for (int i = 0; i < _visionConeResolution; i++)
            {
                float angle = (float) i / _visionConeResolution * 2f * Mathf.PI;
                float u = Mathf.Cos(angle) * 0.5f + 0.5f;
                float v = Mathf.Sin(angle) * 0.5f + 0.5f;
                uv[i + 1] = new Vector2(u, v);
            }

            _visionConeMesh.uv = uv;

            _visionConeMesh.uv = uv;
            _meshFilter.mesh = _visionConeMesh;
        }

        private class VisualConeRaycastData
        {
            public float Sin;
            public float Cos;
        }
    }
}