using System.Collections.Generic;
using Source.Scripts.Utils.SO;
using UnityEngine;

namespace Source.Modules.AudioModule.Scripts
{
    [CreateAssetMenu(fileName = "SoundContainer", menuName = "Setups/Sound/SoundContainer")]
    public class SoundContainer : LoadableScriptableObject<SoundContainer>
    {
        [SerializeField] private List<Sound> _sounds;
        public IReadOnlyCollection<Sound> Sounds => _sounds.AsReadOnly();
    }
}