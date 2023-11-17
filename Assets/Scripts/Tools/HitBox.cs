using Interfaces;
using UnityEngine;
using VisitableComponents;

namespace Tools
{
    public class HitBox : MonoBehaviour, IVisitable
    {
        [SerializeField] private HealthComponent _healthComponent;

        public void Accept(IVisitor visitor)
        {
            Debug.LogError("ACCEPT");
            _healthComponent.Accept(visitor);
        }
    }
}
