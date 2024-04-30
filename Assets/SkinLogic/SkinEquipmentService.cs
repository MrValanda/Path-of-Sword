using System;

namespace Source.Scripts.SkinLogic
{
    public class SkinEquipmentService 
    {
        public event Action<UnitSkinParts, SkinPart> ChangedSkinParts; 

        public void ChangeSkinPart(UnitSkinParts unitSkinParts, params SkinPart[] skinParts)
        {
            foreach (SkinPart skinPart in skinParts)
            {
                unitSkinParts.UpdateSkinPart(skinPart);
                ChangedSkinParts?.Invoke(unitSkinParts, skinPart);
            }
        }
        
        public void UpgradeSkinPart(UnitSkinParts unitSkinParts, SkinPart skinPart)
        {
            unitSkinParts.Upgrade(skinPart);
        }
    }
}
