using DG.Tweening;
using UnityEngine;

namespace Source.CodeLibrary.Extension
{
    public static class TweenExtensions
    {
        public static Component KillTween(this Component component)
        {
            component.DOPause();
            component.DOKill();
            return component;
        }
        
        public static T KillTween<T>(this Component component) where T : class
        {
            component.DOPause();
            component.DOKill();
            return component as T;
        }

        public static Material KillTween(this Material material)
        {
            material.DOPause();
            material.DOKill();
            return material;
        }
    }
}