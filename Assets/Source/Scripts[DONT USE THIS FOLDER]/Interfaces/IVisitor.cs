using Source.Modules.HealthModule.Scripts;
using Source.Scripts.Enemy;
using Source.Scripts.VisitableComponents;
using VisitableComponents;

namespace Interfaces
{
    public interface IVisitor
    {
        public void Visit(HealthComponent healthComponent);
        public void Visit(Animation animation);
    }
}
