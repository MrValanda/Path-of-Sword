using Source.Modules.HealthModule.Scripts;
using Source.Scripts.Enemy;

namespace Interfaces
{
    public interface IVisitor
    {
        public void Visit(HealthComponent healthComponent);
        public void Visit(Animation animation);
    }
}
