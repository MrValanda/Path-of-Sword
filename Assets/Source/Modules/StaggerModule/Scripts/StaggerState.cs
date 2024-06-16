using Source.Scripts;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Modules.StaggerModule.Scripts
{
    public class StaggerState : State
    {
        [SerializeField] private float _impactWeight;
        [SerializeField] private float _impactWeightExit;
       
       
        protected override void OnEnter()
        {
            if (_entity.Contains<StaggerHandler>() == false)
            {
                StaggerHandler staggerHandler = new StaggerHandler();
                _entity.Add(staggerHandler);
                staggerHandler.Initialize(_entity);
            }

            _entity.Get<StaggerHandler>().SetImpactWeight(_impactWeight);
        }
        
        protected override void OnExit()
        {
            _entity.Get<StaggerHandler>().SetImpactWeight(_impactWeightExit);

        }
    }
}