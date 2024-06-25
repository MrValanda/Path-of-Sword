using System.Collections.Generic;
using Source.Modules.BehaviorTreeModule;

namespace Source.Scripts
{
    public class GameActionContainer
    {
       public List<IGameAction> GameActions { get; private set; }

       public GameActionContainer(){}
        public GameActionContainer(List<IGameAction> gameActions)
        {
            GameActions = gameActions;
        }
    }
}