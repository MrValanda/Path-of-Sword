using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Source.Scripts.Data.Models;
using UnityEngine;

namespace Source.Scripts.SkinLogic
{
    [CreateAssetMenu(fileName = "UnitSkinParts", menuName = "Setups/Skins/UnitSkinParts", order = 0)]
    public class UnitSkinParts : ScriptableObject
    {
        [SerializeField] private string saveKey;

        [SerializeField] [InlineEditor(InlineEditorModes.FullEditor)] 
        private List<SkinPart> skinParts;

        [SerializeField] private AllSkinPartsContainer allSkinPartsContainer;
        public event Action<SkinPart> UpdatedSkinPart;

        public List<SkinPart> SkinParts => _currentSkinParts;
        public string SaveKey => saveKey;

        private List<SkinPart> _currentSkinParts;

        public void Initialize()
        {
            _currentSkinParts = new List<SkinPart>(skinParts);
        }

        public void UpdateSkinPart(SkinPart skinPart)
        {
            if (skinPart == null) return;
            bool contains = false;
            for (int i = 0; i < _currentSkinParts.Count; i++)
                if (_currentSkinParts[i].SkinPartType == skinPart.SkinPartType)
                {
                    _currentSkinParts[i] = skinPart;
                    contains = true;
                }

            if (contains == false) _currentSkinParts.Add(skinPart);
            
            
            UpdatedSkinPart?.Invoke(skinPart);
        }

        public void Upgrade(SkinPart skinPart)
        {
          
        }
    }
}