using Source.Scripts;
using Source.Scripts.EntityLogic;
using UnityEngine;

namespace Source.Modules.StaggerModule.Scripts
{
    public class StaggerState : State
    {
        [SerializeField] private float _impactWeight;
        [SerializeField] private float _protectionImpactWeight;
        protected override void OnEnter()
        {
            if (_entity.Contains<StaggerHandler>() == false)
            {
                StaggerHandler staggerHandler = new StaggerHandler();
                _entity.Add(staggerHandler);
                staggerHandler.Initialize(_entity);
            }

            _entity.Get<StaggerHandler>().SetLayersWeight(_impactWeight,_protectionImpactWeight);
        }
        
        protected override void OnExit()
        {
            _entity.Get<StaggerHandler>().SetLayersWeight(0,0);

        }
    }
}