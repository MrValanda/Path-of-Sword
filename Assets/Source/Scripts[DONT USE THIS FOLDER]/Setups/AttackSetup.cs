using System.Collections.Generic;
using System.Linq;
using Source.Scripts.AnimationEventNames;
using UnityEngine;

namespace Source.Scripts.Setups
{
    [CreateAssetMenu(fileName = "AttackSetup", menuName = "Setups/AttackSetup")]
    public class AttackSetup : ScriptableObject
    {
        [field: SerializeField] public AnimationClip AttackAnimation { get; private set; }
        [field: SerializeField] public float AttackDamage { get; private set; }
        [field: SerializeField] public float AttackDelay { get; private set; }
        public float AttackAnimationTime => AttackAnimation.length;


        private void OnValidate()
        {
            if (AttackAnimation == null) return;
            List<string> functionsName = AttackAnimation.events.Select(x => x.functionName).ToList();
            List<string> desiredEventNames = new List<string>()
            {
                AttackEventNames.AttackDisableName,
                AttackEventNames.AttackEnableName,
            };

            foreach (string desiredEventName in desiredEventNames)
            {
                if (functionsName.Contains(desiredEventName) == false)
                {
                    AttackAnimation = null;
                    Debug.LogError("It is not Attack animation");
                }
            }
        }
    }
}