namespace Source.Modules.EnemyModule.Scripts
{
    public class DamageCalculator
    {
        private readonly int _lvl;
        private readonly float _damageMultiplier;

        public DamageCalculator(float damageMultiplier, int lvl)
        {
            _damageMultiplier = damageMultiplier;
            _lvl = lvl;
        }

        public float CalculateDamage(float newBaseDamage)
        {
            return newBaseDamage + _damageMultiplier * _lvl;
        }
    }
}