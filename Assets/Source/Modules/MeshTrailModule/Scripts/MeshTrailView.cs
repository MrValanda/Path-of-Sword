using UnityEngine;

namespace Source.Modules.MeshTrailModule.Scripts
{
    public class MeshTrailView : MonoBehaviour
    {
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshRenderer _meshRenderer;

        public void Initialize(Mesh mesh, Material trailMaterial)
        {
            _meshFilter.mesh = mesh;
            _meshRenderer.materials = new[]
            {
                trailMaterial, trailMaterial, trailMaterial
            };
        }
    }

    public struct MeshTrailViewSpawnData
    {
        public readonly Transform Target;
        public readonly MeshTrailView MeshTrailView;

        public MeshTrailViewSpawnData(MeshTrailView meshTrailView, Transform target)
        {
            MeshTrailView = meshTrailView;
            Target = target;
        }
    }
}