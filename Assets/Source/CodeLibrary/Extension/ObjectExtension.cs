using System;

namespace Source.CodeLibrary.Extension
{
    public static class ObjectExtension
    {
        public static bool IsNullUniversal<T>(this T instance)
        {
            if (instance is Object unityObject)
                return unityObject.Equals(null) || instance is UnityEngine.Object == false;

            return instance == null;
        }
        
        public static T With<T>(this T self, Action<T> set)
        {
            set?.Invoke(self);
            return self;
        }
        
        public static T With<T>(this T self, Action<T> apply, Func<bool> when)
        {
            if(when())
                apply?.Invoke(self);
            return self;
        }
        
        public static T With<T>(this T self, Action<T> apply, bool when)
        {
            if(when)
                apply?.Invoke(self);
            return self;
        }
    }
}