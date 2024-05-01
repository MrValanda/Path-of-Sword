using System;
using Source.Scripts.Utils.SO;

namespace Source.Scripts.Utils
{
    public static class ScriptablePathGetter
    {
        private const string ResourcePath = "ScriptableObjects";
        
        private static readonly bool isInitialized;
        private static readonly ScriptablePaths scriptablePaths;
        
        static ScriptablePathGetter()
        {
            if (isInitialized == false)
            {
                isInitialized = true;

                string path = $"{ResourcePath}/{nameof(ScriptablePaths)}";
                
                scriptablePaths = UnityEngine.Resources.Load<ScriptablePaths>(path);
            }
        }
        
        public static string GetCustomPathsFromConfig(Type type)
        {
            return scriptablePaths.GetCustomPaths(type);
        }
    }
}
