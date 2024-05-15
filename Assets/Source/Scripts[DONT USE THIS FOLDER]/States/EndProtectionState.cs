using Source.Modules.HealthModule.Scripts;
using Source.Scripts;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Scripts_DONT_USE_THIS_FOLDER_.States
{
    public class EndProtectionState : State
    {
        [SerializeField] private ProtectionState _protectionState;
        
        private void OnDisable()
        {
              _entity.AddOrGet<EntityCurrentStatsData>().DamageReducePercent = _protectionState.PreviousDamageReduce;
        }
    }
}