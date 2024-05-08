using System.Collections.Generic;

namespace Source.Modules.CombatModule.Scripts
{
    public class Container<T>
    {
        public Container(List<T> containerData)
        {
            ContainerData = containerData;
        }

        public List<T> ContainerData { get; private set; }
    }
}