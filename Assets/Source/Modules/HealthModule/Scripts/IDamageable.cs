using System;

namespace Source.Scripts.Interfaces
{
    public interface IDamageable
    {
        public event Action<double> ReceivedDamage;
        public void ApplyDamage(double damage);
    }
}