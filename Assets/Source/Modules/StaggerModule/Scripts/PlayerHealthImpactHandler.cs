using Source.CodeLibrary.ServiceBootstrap;
using Source.Modules.CameraModule.Scripts;
using Source.Modules.HealthModule.Scripts;
using Source.Scripts.EntityLogic;
using Source.Scripts.Interfaces;

namespace Source.Modules.StaggerModule.Scripts
{
    public class PlayerHealthImpactHandler : HealthImpactHandler
    {
        private CameraShakeService _cameraShakeService;
        protected override void OnInitialized()
        {
            _cameraShakeService = ServiceLocator.For(Entity).Get<CameraShakeService>();
        }

        protected override void OnReceivedDamage(double obj)
        {
            if(Entity.Get<EntityCurrentStatsData>().DamageReducePercent == 0) return;
            
            
            _cameraShakeService.Shake(2,0.5f);
        }
    }
}