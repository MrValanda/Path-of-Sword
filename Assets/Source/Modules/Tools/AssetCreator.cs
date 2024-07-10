#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#endif

using UnityEngine;

namespace Source.Scripts.EditorTools
{
    public class AssetCreator<T> where T : ScriptableObject, new()
    {
        public T CreateAsset(string path, string name)
        {
            T createdAsset = ScriptableObject.CreateInstance<T>();
            createdAsset.name = name;
#if UNITY_EDITOR
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            AssetDatabase.CreateAsset(createdAsset, path + "/" + name + ".asset");
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
#endif

            return createdAsset;
        }
    }
}