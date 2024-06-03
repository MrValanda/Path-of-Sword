using System;
using Source.Modules.CombatModule.Scripts.Parry;
using Source.Scripts.EntityLogic;
using Source.Scripts.VisitableComponents;

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