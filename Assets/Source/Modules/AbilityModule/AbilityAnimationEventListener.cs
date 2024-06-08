using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Source.Scripts.AnimationEventListeners
{
    public class AbilityAnimationEventListener : MonoBehaviour
    {
        public event Action AbilityDestroyEvent;

        [Preserve]
        private void DestroyAnimation()
        {
            AbilityDestroyEvent?.Invoke();
        }
    }
}