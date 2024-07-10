#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.Modules.WeaponModule.Scripts;
using Source.Scripts.Utils;
using UnityEngine;

public class EnemyValidator : MonoBehaviour
{
    [SerializeField,OnInspectorGUI(nameof(Validate))] private ComponentContainerMonoLinker _componentContainerMonoLinker;

    private readonly List<Type> _validationTypes = new List<Type>()
    {
        typeof(SkinnedMeshRenderer),
        typeof(WeaponLocator),
    };
    
    private void Validate()
    {
        List<Component> cloneMono = _componentContainerMonoLinker.GetCloneMono();
        foreach (Type validationType in _validationTypes)
        {
            if (cloneMono.Any(x => x.GetType() == validationType) == false)
            {
                Debug.LogError($"Enemy not contains {validationType.Name}");
            }
        }

    }
}
#endif