using System.IO;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Scripts.Utils.SO
{
    public abstract class LoadableScriptableObject<T> : SerializedScriptableObject where T : SerializedScriptableObject
    {
        protected const string ResourcePath = "";
        
        private static T _cachedInstance = null;
        private static int _referenceCount = 0;
        private static bool _isNotReleasable = false;
        private static ResourceRequest _loadRequest = null;

        public static async UniTask<T> GetInstanceAsync(bool canBeReleased = true)
        {
            _isNotReleasable = !canBeReleased;
            _referenceCount++;
            
            if (_cachedInstance == null)
            {
                string customPath =  ScriptablePathGetter.GetCustomPathsFromConfig(typeof(T));
                string path = $"{customPath}{(customPath.Equals("") ? "" : "/")}{typeof(T).Name}";
                
                if (_loadRequest == null)
                    _loadRequest = Resources.LoadAsync<T>(path);

                await _loadRequest;

                _cachedInstance = (T)_loadRequest.asset;

                if (_cachedInstance == null)
                    throw new FileLoadException(
                        $"Can not load <SO> with type:[{typeof(T).Name}] by resource path:[{path}]");
            }
            
            return _cachedInstance;
        }

        public static T GetInstance(bool canBeReleased = true)
        {
            _isNotReleasable = !canBeReleased;
            _referenceCount++;
            
            if (_cachedInstance == null)
            {
                string customPath = ScriptablePathGetter.GetCustomPathsFromConfig(typeof(T));
                string path = $"{customPath}{(customPath.Equals("") ? "" : "/")}{typeof(T).Name}";
                
                _cachedInstance = UnityEngine.Resources.Load<T>(path);

                if (_cachedInstance == null)
                    throw new FileLoadException($"Can not load <SO> with type:[{typeof(T).Name}] by resource path:[{path}]");
            }
            
            return _cachedInstance;
        }
        
        public static T ReleaseInstance()
        {
            if (_isNotReleasable)
                return null;
            
            _referenceCount--;

            if (_referenceCount <= 0 && _cachedInstance != null)
            {
                Resources.UnloadAsset(_cachedInstance);
                _cachedInstance = null;
                _loadRequest = null;
            }

            return null;
        }
        
        public static void ClearAllReferences()
        {
            if (_isNotReleasable)
                return;
            
            _referenceCount = 0;

            if (_cachedInstance != null)
            {
                Resources.UnloadAsset(_cachedInstance);
                _cachedInstance = null;
                _loadRequest = null;
            }
        }

        public T Release()
        {
            if (_isNotReleasable)
                return null;
                
            _referenceCount--;

            if (_referenceCount <= 0 && _cachedInstance != null)
            {
                Resources.UnloadAsset(_cachedInstance);
                _cachedInstance = null;
                _loadRequest = null;
            }

            return null;
        }
        
        protected static string GetCustomPath() => "";
    }
}