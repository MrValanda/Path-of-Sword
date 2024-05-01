using Source.Scripts.InterfaceLinker;
using UnityEngine;

namespace Source.Scripts.Utils
{
    public class TakeDamageMaterialView : MonoBehaviour
    {
        [SerializeField] private MaterialColorChanger _materialColorChanger;
        [SerializeField] private DamageableLinker _damageableLinker;

        private void OnEnable() => _damageableLinker.Value.ReceivedDamage += OnReceivedDamage;

        private void OnDisable() => _damageableLinker.Value.ReceivedDamage -= OnReceivedDamage;

        private void OnReceivedDamage(double damage) => _materialColorChanger.Change();
    }
}