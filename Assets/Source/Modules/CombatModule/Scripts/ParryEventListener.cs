using Source.Modules.MoveSetModule.Scripts;
using Source.Scripts.EntityLogic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Source.Modules.CombatModule.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class ParryEventListener : MonoBehaviour
    {
        [SerializeField] private Entity _entity;

        [Preserve]
        private void ParryType(int parryIndex)
        {
            _entity.AddOrGet<CurrentAttackData>().CurrentParryAnimationIndex = (ParryAnimationIndex) parryIndex;
        }
    }
}