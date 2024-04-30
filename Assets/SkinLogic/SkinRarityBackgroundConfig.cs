using System;
using System.Collections.Generic;
using System.Linq;
using Source.Scripts.Utils.SO;
using UnityEngine;

namespace Source.Scripts.SkinLogic
{
    [CreateAssetMenu(fileName = "SkinRarityBackgroundConfig", menuName = "Setups/Rarity/SkinRarityBackgroundConfig", order = 0)]
    public class SkinRarityBackgroundConfig : LoadableScriptableObject<SkinRarityBackgroundConfig>
    {
        [field: SerializeField] public List<SkinRarityPair> Pairs { get; private set; } = new();

        public SkinRarityPair Get(SkinRarityType rarityType)
        {
            SkinRarityPair pair = Pairs.FirstOrDefault(pr => pr.RarityType == rarityType);

            if (pair == default)
                throw new ArgumentException("Vpadlu pisat'");

            return pair;
        }
        
        [Serializable]
        public class SkinRarityPair
        {
            [field: SerializeField] public SkinRarityType RarityType { get; private set; }
            [field: SerializeField] public Sprite BackgroundIcon { get; private set; }
        }
    }
}