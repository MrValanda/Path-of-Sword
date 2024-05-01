using System;

namespace Source.Scripts.Interfaces
{
    public interface IDying
    {
        public event Action<IDying> Dead;
        public bool IsDead { get; }
    }
}