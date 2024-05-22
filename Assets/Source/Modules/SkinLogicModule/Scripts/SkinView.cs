using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using SkinLogic;
using Source.Scripts.SkinLogic;
using UnityEngine;

namespace Source.Modules.SkinLogicModule.Scripts
{
    public class SkinView : SerializedMonoBehaviour
    {
        [OdinSerialize] private Dictionary<SkinPartType, WearSkinPart> _wearSkinParts;
        [SerializeField] private List<UnitSkinParts> defaultSkinParts;

        public UnitSkinParts UnitSkinParts { get; private set; }
        
        public void Init(UnitSkinParts unitSkinParts)
        {
            if (UnitSkinParts != null)
            {
                HideSkin();
                UnitSkinParts.UpdatedSkinPart -= OnUpdatedSkinPart;
            }
            UnitSkinParts = unitSkinParts;
            UnitSkinParts.Initialize();
            UnitSkinParts.UpdatedSkinPart += OnUpdatedSkinPart;
            InitView();
        }

        private void Start()
        {
            SetRandomSkin();
        }

        [Button]
        private void OnUpdatedSkinPart(SkinPart obj)
        {
            if (obj.SkinPartType == SkinPartType.Personality)
            {
                foreach (KeyValuePair<SkinPartType, WearSkinPart> wearSkinPart in _wearSkinParts)
                    wearSkinPart.Value.Clear();
            }
            else
            {
                if (_wearSkinParts.ContainsKey(SkinPartType.Personality))
                    _wearSkinParts[SkinPartType.Personality].Clear();
            }

            TryUpdateSkinPart(obj);
        }

        public void SetRandomSkin()
        {
            if (defaultSkinParts is { Count: > 0 }) Init(defaultSkinParts[Random.Range(0, defaultSkinParts.Count)]);
        }
            private void OnDestroy()
        {
            if (UnitSkinParts == null) return;

            UnitSkinParts.UpdatedSkinPart -= OnUpdatedSkinPart;
            UnitSkinParts = null;
        }

        private void InitView()
        {
            if (UnitSkinParts == null) return;

            foreach (SkinPart skinPart in UnitSkinParts.SkinParts) OnUpdatedSkinPart(skinPart);
        }

        public void ClearSkinPart(SkinPartType skinPartType)
        {
            if (_wearSkinParts.ContainsKey(skinPartType)) _wearSkinParts[skinPartType].Clear();
        }

        public void HideSkin()
        {
            foreach (KeyValuePair<SkinPartType, WearSkinPart> wearSkinPart in _wearSkinParts)
                wearSkinPart.Value.Clear();
        }

        public void TryUpdateSkinPart(SkinPart skinPart)
        {
            if (_wearSkinParts.TryGetValue(skinPart.SkinPartType, out WearSkinPart wearSkinPart))
                wearSkinPart.UpdateSkinPart(skinPart);
        }

        public bool ContainsSkinPart(SkinPart skinPart)
        {
            return UnitSkinParts.SkinParts.FirstOrDefault(x => x.SkinPartName.Equals(skinPart.SkinPartName)) != null;
        }
    }
}