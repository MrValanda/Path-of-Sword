using UnityEngine;

namespace Source.Scripts.Utils
{
    public static class MeterialChanger
    {
        public static void ChangeMaterial(GameObject _gameObject, Material _material)
        {
            Renderer[] renderers = _gameObject.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                renderer.material = _material;
            }
        }
    }
}
