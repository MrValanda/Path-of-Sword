using BehaviorDesigner.Runtime;

namespace Source.Scripts.NPC.Collector
{
    public static class BehaviorTreeExtension
    {
        public static void InitVariable<T, T1>(this BehaviorTree behaviorTree,string nameVariable, T1 value) where T : SharedVariable<T1>, new()
        {
            behaviorTree.SetVariable(nameVariable, new T() {Value = value});
        }
    }
}