using System;
using Source.Modules.Tools;
using UnityEngine.Scripting;

namespace Source.Modules.CombatModule.Scripts
{
    public class DodgeEventListener : OptimizedMonoBehavior
    {
        public event Action DodgeEnded;
        public event Action StartListenDodgeAttack;
        public event Action StartListenDodge;

        [Preserve]
        private void DodgeEnd()
        {
            DodgeEnded?.Invoke();
        }

        [Preserve]
        private void StartListenAttack()
        {
            StartListenDodgeAttack?.Invoke();
        }

        [Preserve]
        private void ListenDodge()
        {
            StartListenDodge?.Invoke();
        }
    }
}