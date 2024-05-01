using System;
using UnityEngine;

namespace Source.Scripts.AnimationEventListeners
{
    [RequireComponent(typeof(Animator))]
    public class InteractAnimationEventListener : MonoBehaviour
    {
        public event Action Interacted;

        public void Interact()
        {
            Interacted?.Invoke();
        }
    }
}