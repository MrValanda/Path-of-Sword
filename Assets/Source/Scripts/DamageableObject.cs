using System;
using DG.Tweening;
using Source.Scripts.Interfaces;
using UnityEngine;

namespace Source.Scripts
{
    public class DamageableObject : MonoBehaviour, IDamageable, IInteractable
    {
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;
        public event Action<double> ReceivedDamage;
        public bool IsInteractionAvailable { get; }
        private Sequence _sequence;

        public void ApplyDamage(double damage)
        {
            if (_skinnedMeshRenderer != null)
            {
                _sequence?.Kill();
                _sequence = DOTween.Sequence();
                _sequence.Append(DOTween.To(() => _skinnedMeshRenderer.GetBlendShapeWeight(0),
                    x => _skinnedMeshRenderer.SetBlendShapeWeight(0, x), 100, 0.1f));
                _sequence.Append(DOTween.To(() => _skinnedMeshRenderer.GetBlendShapeWeight(0),
                    x => _skinnedMeshRenderer.SetBlendShapeWeight(0, x), 0, 0.1f));
            }

            ReceivedDamage?.Invoke(damage);
        }
    }
}