using Source.CodeLibrary.ServiceBootstrap;
using Source.Modules.CombatModule.Scripts;
using Source.Scripts.EntityLogic;
using UnityEngine;
using UnityEngine.Scripting;

namespace Source.Modules.AudioModule.Scripts
{
    [RequireComponent(typeof(Animator))]
    public class EquipmentAudioHandler : MonoBehaviour
    {
        [SerializeField] private Entity _sender;
        private SoundPlayer _soundPlayer;

        [Preserve]
        private void Whoosh()
        {
            if (_sender.TryGet(out CurrentAttackData currentAttackData) == false)
            {
                return;
            }
            PlaySound(currentAttackData.CurrentHitInfo.WhooshSoundsType);
        }

        private void PlaySound(SoundType soundType)
        {
            _soundPlayer ??= ServiceLocator.ForSceneOf(this).Get<SoundPlayer>();
            _soundPlayer.PlaySoundByType(soundType);
        }
    }
}