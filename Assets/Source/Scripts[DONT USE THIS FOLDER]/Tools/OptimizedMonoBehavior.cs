using UnityEngine;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.Tools
{
    public class OptimizedMonoBehavior : MonoBehaviour
    {
        [HideInInspector] public new Transform transform;

        [HideInInspector] public new GameObject gameObject;

        protected  virtual void OnValidate()
        {
            transform = base.transform;
            gameObject = base.gameObject;
        }
    }
}