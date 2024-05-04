using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Utils;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using UnityEngine;

namespace Source.Scripts.Utils
{
    public class ComponentContainerMonoLinker : MonoBehaviour
    {
        [SerializeField]
        private List<MonoBehaviour> _monoBehaviours = new List<MonoBehaviour>();

        public ComponentContainer ComponentsContainer { get; private set; }


        public void Init()
        {
            Debug.LogError($"Init{_monoBehaviours.Count}");
            ComponentsContainer = new ComponentContainer();
            foreach (var monoBehaviour in _monoBehaviours)
            {
                Type type = monoBehaviour.GetType();
                ComponentsContainer.AddComponent(monoBehaviour, type);
                foreach (Type interfaceType in type.GetInterfaces())
                {
                    ComponentsContainer.AddComponent(monoBehaviour, interfaceType);
                }

                ComponentsContainer.AddComponent(monoBehaviour, type);
            }

            _monoBehaviours.ForEach(x => ComponentsContainer.AddComponent(x, x.GetType()));
        }
    }
}