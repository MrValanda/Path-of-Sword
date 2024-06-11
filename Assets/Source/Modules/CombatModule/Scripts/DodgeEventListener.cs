using System;
using Source.Scripts_DONT_USE_THIS_FOLDER_.Tools;
using UnityEngine.Scripting;

namespace Source.Modules.CombatModule.Scripts
{
    public class DodgeEventListener : OptimizedMonoBehavior
    {
        public event Action DodgeEnded;
        public event Action StartListenDodgeAttack;

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
    }
}