using System.Collections.Generic;
using Source.Modules.BehaviorTreeModule;

namespace Source.Scripts
{
    public class GameConditionsContainer
    {
        public List<IGameCondition> GameConditions { get; private set; }

        public GameConditionsContainer()
        {
        }

        public GameConditionsContainer(List<IGameCondition> gameConditions)
        {
            GameConditions = gameConditions;
        }
    }
}