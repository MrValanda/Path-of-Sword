using UniRx;
using UnityEngine;

namespace Source.CodeLibrary.Mesh
{
    public class MeshVisibilityChecker : MonoBehaviour
    {
        [SerializeField] private GameObject _hidableObject;
        public ReactiveProperty<bool> IsVisible { get; private set; } = new();
        
        private void OnBecameVisible()
        {
            if(_hidableObject != null)
                _hidableObject.SetActive(IsVisible.Value = true);
        }

        private void OnBecameInvisible()
        {
            if(_hidableObject != null)
                _hidableObject.SetActive(IsVisible.Value = false);
        }
    }
}
