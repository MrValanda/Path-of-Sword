using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Profiling;

namespace Source.Scripts.Utils
{
    public class MaterialColorChanger : MonoBehaviour
    {
        [SerializeField] private Color _targetColor;
        [SerializeField] private float _delayAfterTargetColorReached = 0.05f;
        [SerializeField] private float _changeDuration = 0.07f;
        [SerializeField] private Material _damageMaterial;
        [SerializeField] private Renderer[] _renderersForChangingMaterial;

        private readonly Dictionary<Material, Color> _cashedColors = new();

        private void Start()
        {
            foreach (Renderer renderer in _renderersForChangingMaterial)
            {
                Material instantiate = Instantiate(_damageMaterial);
                renderer.materials = renderer.materials.Append(instantiate).ToArray();
                instantiate.color = new Color(_targetColor.r, _targetColor.g, _targetColor.b, 0);
                _cashedColors.Add(instantiate, instantiate.color);
            }
        }

        [Button]
        public void Change()
        {
            Sequence sequence = DOTween.Sequence();

            foreach ((Material material, Color _) in _cashedColors)
            {
                sequence.Join(FadeMaterialColor(material, _targetColor));
            }

            sequence.AppendInterval(_delayAfterTargetColorReached);

            if (_cashedColors.Count > 0)
            {
                sequence.AppendInterval(0);
            }

            foreach ((Material material, Color color) in _cashedColors)
            {
                sequence.Join(FadeMaterialColor(material, color));
            }
        }

        private TweenerCore<Color, Color, ColorOptions> FadeMaterialColor(Material material, Color color)
        {
            return material.DOFade(color.a, _changeDuration);
        }
    }
}