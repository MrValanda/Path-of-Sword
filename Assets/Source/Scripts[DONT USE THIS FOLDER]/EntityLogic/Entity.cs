using Source.Scripts.Utils;
using UnityEngine;

namespace Source.Scripts.EntityLogic
{
    
    [RequireComponent(typeof(ComponentContainerMonoLinker))]
    public class Entity : OptimizedMonoBehavior
    {
        public ComponentContainerMonoLinker ComponentContainerMonoLinker
        {
            get
            {
                if (_componentContainerMonoLinker == null)
                {
                    _componentContainerMonoLinker = GetComponent<ComponentContainerMonoLinker>();
                    _componentContainerMonoLinker.Init();
                    OnComponentContainerInitialize();
                }

                return _componentContainerMonoLinker;
            }
        }

        private ComponentContainerMonoLinker _componentContainerMonoLinker;
        
        protected virtual void OnComponentContainerInitialize(){}
    }
}