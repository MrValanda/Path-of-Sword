using Source.Scripts.AttackPointCalculators;
using Source.Scripts.Interfaces;

namespace Source.Scripts.Enemy
{
    public class RangeEnemy : SkeletonEnemy
    {
        public override IAttackPointCalculator GetAttackPointCalculator()
        {
            return new RangeAttackPointCalculator(Target.transform, transform);
        }
    }
}