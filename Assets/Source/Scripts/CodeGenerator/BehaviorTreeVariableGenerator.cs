using System.Linq;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Source.Scripts.CodeGenerator
{
    public class BehaviorTreeVariableGenerator : MonoBehaviour
    {
        [Button]
        private void Generate(ExternalBehaviorTree behaviorTree,string path)
        {
            ClassGenerator classGenerator = new ClassGenerator();

            classGenerator.Generate(behaviorTree.name + "Variables",
                behaviorTree.BehaviorSource.GetAllVariables().Where(x => x != null).Select(x => x.Name).ToList(),path);
        }
    }
}
