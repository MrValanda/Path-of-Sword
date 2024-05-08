using System;

namespace Source.Modules.CombatModule.Scripts
{
    [Serializable]
    public class InfinitySuccessCondition : ICondition
    {
        public bool GetStatus()
        {
            return true;
        }
    }
}