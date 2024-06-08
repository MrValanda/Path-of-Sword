using System;
using Source.CodeLibrary.ServiceBootstrap;
using UnityEngine;

namespace Source.Scripts.AnimationEventListeners
{
    [RequireComponent(typeof(Animator))]
    public class AbilityEventListener : MonoBehaviour
    {
        public event Action AbilityStarted;
        public event Action AbilityEnded;
        public event Action PreparationStarted;
        public event Action PreparationEnded;


        private bool _needSound;

        private void AbilityStart()
        {
            AbilityStarted?.Invoke();
        }

        private void AbilityEnd()
        {
            AbilityEnded?.Invoke();
        }

        private void PreparationStart()
        {
            PreparationStarted?.Invoke();
        }

        private void PreparationEnd()
        {
            PreparationEnded?.Invoke();
        }
    }
}