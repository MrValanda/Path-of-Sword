using BehaviorDesigner.Runtime;

namespace Source.Scripts.BehaviorTreeEventSenders
{
    public class NeedStayAfkBehaviorTreeEventSender
    {
        private const string NeedStayAfkEventName = "NeedStayAfk";
        private readonly BehaviorTree _behaviorTree;

        public NeedStayAfkBehaviorTreeEventSender(BehaviorTree behaviorTree)
        {
            _behaviorTree = behaviorTree;
        }

        public void SendEvent(float afkTime)
        {
            if (afkTime == 0) return;
            _behaviorTree.SendEvent<object>(NeedStayAfkEventName, afkTime);
        }
    }
}