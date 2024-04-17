using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Source.Scripts.Utils.SO
{
    [CreateAssetMenu(fileName = "ScriptablePaths", menuName = "Setups/ScriptablePaths")]
    public class ScriptablePaths : LoadableScriptableObject<ScriptablePaths>
    {
        [OdinSerialize] private readonly List<CustomPath> _customPaths = new();

#if UNITY_EDITOR
        private void OnValidate()
        {
            foreach (CustomPath customPath in _customPaths)
            {
                if (customPath.ClassType != null)
                    customPath.ConfigType = customPath.ClassType.Name;
            }
        }
#endif

        public string GetCustomPaths([NotNull] Type type)
        {
            if (type == null) 
                throw new ArgumentNullException(nameof(type) + "is null ");
            
            string path = "";
            
            foreach (CustomPath customPath in _customPaths.Where(p => type.Name == p.ConfigType))
            {
                path = customPath.Path;
            }
            
            return path;
        }
    }

    [Serializable]
    public class CustomPath
    {
        [ValueDropdown(nameof(FilterTypes))] public Type ClassType;
        public string Path;
        [HideInInspector] public string ConfigType;

        private List<Type> FilterTypes()
        {
            return Assembly.GetAssembly(typeof(ScriptablePaths)).GetTypes().Where(x => x.BaseType != null && x.BaseType.Name.Contains("LoadableScriptable")).ToList();
        }
    }
}