using System;
using System.Collections.Generic;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Utils;
#if UNITY_EDITOR
#endif
using UnityEngine;

namespace Source.Scripts.Utils
{
    public class ComponentContainerMonoLinker : MonoBehaviour
    {
        [SerializeField]
        private List<Component> _monoBehaviours = new List<Component>();

        public ComponentContainer ComponentsContainer { get; private set; }


        public void Init()
        {
            ComponentsContainer = new ComponentContainer();
            foreach (var monoBehaviour in _monoBehaviours)
            {
                Type type = monoBehaviour.GetType();
                ComponentsContainer.AddComponent(monoBehaviour, type);
                foreach (Type interfaceType in type.GetInterfaces())
                {
                    ComponentsContainer.AddComponent(monoBehaviour, interfaceType);
                }
                ComponentsContainer.AddComponent(monoBehaviour,  type.BaseType);
                
                ComponentsContainer.AddComponent(monoBehaviour, type);
            }

            _monoBehaviours.ForEach(x => ComponentsContainer.AddComponent(x, x.GetType()));
        }
    }
}