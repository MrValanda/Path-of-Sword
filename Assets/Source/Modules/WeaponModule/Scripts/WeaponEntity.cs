using Source.Scripts.EntityLogic;
using XftWeapon;

namespace Source.Scripts.WeaponModule
{
    public class WeaponEntity : Entity
    {
        protected override void OnComponentContainerInitialize()
        {
            XWeaponTrail[] componentsInChildren = GetComponentsInChildren<XWeaponTrail>();
            this.AddOrGet<WeaponTrailContainer>().XWeaponTrails = componentsInChildren;
        }
    }
}