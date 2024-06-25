using System;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts.EntityLogic;

namespace VisitableComponents
{
    public class EnemyHealthComponent : HealthComponent
    {
        private IDisposable _disposable;
        private void Awake()
        {
            Entity.Add(new ParryComponent() {WhoParryEntity = Entity});
        }
    }
}