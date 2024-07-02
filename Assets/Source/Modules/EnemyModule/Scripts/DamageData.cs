namespace Source.Modules.EnemyModule.Scripts
{
    public abstract class ValueCalculator
    {
        public abstract float CalculateValue(float baseValue);
    }

    public class DamageCalculator : ValueCalculator
    {
        private readonly int _lvl;
        private readonly float _damageMultiplier;

        public DamageCalculator(float damageMultiplier, int lvl)
        {
            _damageMultiplier = damageMultiplier;
            _lvl = lvl;
        }

        public override float CalculateValue(float newBaseDamage)
        {
            return newBaseDamage + _damageMultiplier * _lvl;
        }
    }
}