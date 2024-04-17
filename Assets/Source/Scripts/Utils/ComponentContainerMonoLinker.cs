using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.OdinInspector.Editor;
#endif
using UnityEngine;

namespace Source.Scripts.Utils
{
    public class ComponentContainerMonoLinker : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField, OnCollectionChanged(Before = nameof(InitComponents))]
#endif
        private List<MonoBehaviour> _monoBehaviours = new List<MonoBehaviour>();

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

                ComponentsContainer.AddComponent(monoBehaviour, type);
            }

            _monoBehaviours.ForEach(x => ComponentsContainer.AddComponent(x, x.GetType()));
        }

#if UNITY_EDITOR
        private void InitComponents(CollectionChangeInfo collectionChangeInfo, object value)
        {
            switch (collectionChangeInfo.ChangeType)
            {
                case CollectionChangeType.Add:
                case CollectionChangeType.Insert:
                    if (_monoBehaviours.Any(x => x.GetType() == collectionChangeInfo.Value.GetType()))
                    {
                    }

                    break;
            }
        }
#endif
    }
}