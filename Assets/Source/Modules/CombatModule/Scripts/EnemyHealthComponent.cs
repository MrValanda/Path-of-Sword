using Source.Modules.CombatModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.VisitableComponents;

namespace VisitableComponents
{
    public class EnemyHealthComponent : HealthComponent
    {
        private void Awake()
        {
            Entity.Add(new ParryComponent() {WhoParryEntity = Entity});
        }
    }
}