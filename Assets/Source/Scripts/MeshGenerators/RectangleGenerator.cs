using Sirenix.OdinInspector;
using Source.Scripts.Setups;
using UnityEngine;
using UnityEngine.Rendering;

namespace Source.Scripts
{
    public class RectangleGenerator : MeshGenerator
    {
        [SerializeField] private Material _rectangleMaterial;
        [SerializeField] private float _maxDistance;
        [SerializeField] private float _width;
        [SerializeField] private LayerMask _obstructingLayer;

        private Mesh _rectangleMesh;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private Transform _selfTransform;

        private Vector3[] _vertices;
        private int[] _triangles;

#if UNITY_EDITOR
        [Button]
        private void DebugDraw()
        {
            if (Application.isPlaying)
                return;

            RecalculateValues();
            DrawRectangle();
        }
#endif

        public override void Init(IndicatorDataSetup indicatorDataSetup, LayerMask obstructingLayer)
        {
            if (indicatorDataSetup is not RectangleIndicatorDataSetup)
            {
                Debug.LogError($"{indicatorDataSetup} is not {nameof(RectangleIndicatorDataSetup)}");
                return;
            }

            RectangleIndicatorDataSetup rectangleIndicatorDataSetup = (RectangleIndicatorDataSetup) indicatorDataSetup;
            _maxDistance = rectangleIndicatorDataSetup.MaxDistance;
            _width = rectangleIndicatorDataSetup.Width;
            _obstructingLayer = obstructingLayer;
            MeshMaterial ??= Instantiate(_rectangleMaterial);
            MeshMaterial.SetColor("_BorderColor", indicatorDataSetup.BorderColor);
            MeshMaterial.SetColor("_StartColor", indicatorDataSetup.StartColor);
            MeshMaterial.SetColor("_EndColor", indicatorDataSetup.EndColor);

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

            _meshRenderer.material = MeshMaterial ? MeshMaterial : _rectangleMaterial;

            if (_meshFilter == null)
                _meshFilter = _selfTransform.TryGetComponent(out MeshFilter meshFilter)
                    ? meshFilter
                    : _selfTransform.gameObject.AddComponent<MeshFilter>();

            _rectangleMesh = new Mesh();

            RaycastHit hit;
            if (Physics.BoxCast(_selfTransform.position, new Vector3(_width / 2, _width / 2, _width / 2),
                _selfTransform.forward, out hit, _selfTransform.rotation, _maxDistance, _obstructingLayer))
            {
                float hitDistance = Vector3.Distance(_selfTransform.position, hit.point);
                _vertices = new Vector3[4]
                {
                    new Vector3(-_width / 2, 0f, 0f),
                    new Vector3(-_width / 2, 0f, hitDistance),
                    new Vector3(_width / 2, 0f, hitDistance),
                    new Vector3(_width / 2, 0f, 0f)
                };
                CalculateBorderSize(hitDistance, _width);
            }
            else
            {
                _vertices = new Vector3[4]
                {
                    new Vector3(-_width / 2, 0f, 0f),
                    new Vector3(-_width / 2, 0f, _maxDistance),
                    new Vector3(_width / 2, 0f, _maxDistance),
                    new Vector3(_width / 2, 0f, 0f)
                };
                CalculateBorderSize(_maxDistance, _width);
            }

            _triangles = new int[6]
            {
                0, 1, 2,
                0, 2, 3
            };
        }

        private void Update()
        {
            RecalculateValues();
            DrawRectangle();
        }

        private void DrawRectangle()
        {
            _rectangleMesh.Clear();
            _rectangleMesh.vertices = _vertices;
            _rectangleMesh.triangles = _triangles;

            Vector2[] uv = new Vector2[4]
            {
                new Vector2(0f, 0f),
                new Vector2(0f, 1f),
                new Vector2(1f, 1f),
                new Vector2(1f, 0f)
            };

            _rectangleMesh.uv = uv;
            _meshFilter.mesh = _rectangleMesh;
        }

        private void CalculateBorderSize(float distance, float width)
        {
            float distanceInitial = 1f;
            float widthInitial = 1f;

            float newX = 0.001f * (distance / distanceInitial);
            float newY = 0.001f * (width / widthInitial);

            MeshMaterial?.SetVector("_BorderSize", new Vector4(newX, newY, 0, 0));
        }
    }
}