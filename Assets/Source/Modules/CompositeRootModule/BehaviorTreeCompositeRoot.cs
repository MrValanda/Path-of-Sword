using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Source.Modules.CompositeRootModule
{
    public abstract class BehaviorTreeCompositeRoot : MonoBehaviour
    {
        [SerializeField] protected BehaviorTree BehaviorTree;
        public abstract void Compose();
    }
}