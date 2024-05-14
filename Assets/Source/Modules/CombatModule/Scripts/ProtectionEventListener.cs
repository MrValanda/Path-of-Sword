using System;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using UnityEngine.Scripting;

namespace Source.Modules.CombatModule.Scripts
{
    public class ProtectionEventListener : OptimizedMonoBehavior
    {
        public event Action ParryStarted;
        public event Action ParryEnded;
        public event Action ProtectionStarted;
        public event Action ProtectionEnded;

        [Preserve]
        private void StartParry()
        {
            ParryStarted?.Invoke();
        }

        [Preserve]
        private void StopParry()
        {
            ParryEnded?.Invoke();
        }

        [Preserve]
        private void ProtectionStart()
        {
            ProtectionStarted?.Invoke();
        }

        [Preserve]
        private void ProtectionEnd()
        {
            ProtectionEnded?.Invoke();
        }
    }
}