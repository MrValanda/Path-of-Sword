using Source.CodeLibrary.ServiceBootstrap.SceneContainers;
using UnityEditor;
using UnityEngine;

namespace Source.CodeLibrary.ServiceBootstrap
{
    public class BootstrapEditor
#if UNITY_EDITOR
        : Editor
#endif
    {
#if UNITY_EDITOR
        [MenuItem(BootstrapConstants.MenuPath + "Global")]
        private static void AddGlobal() => new GameObject(BootstrapConstants.GlobalServiceLocatorName, typeof(GlobalBootstrapper));

        private static void NormalizeHierarchy(GameObject bootstrapper)
        {
            if(Application.isPlaying) return;
            
            GameObject parent = GameObject.Find(BootstrapConstants.ParentName);

            if (parent == null)
            {
                parent = new GameObject(BootstrapConstants.ParentName);
                parent.transform.position =
                parent.transform.eulerAngles = Vector3.zero;
                parent.transform.localScale = Vector3.one;
            }
            
            bootstrapper.transform.SetParent(parent.transform);
        }
#endif
    }
}
