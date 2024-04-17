using Source.Scripts.Interfaces;

namespace Source.Scripts.InterfaceLinker
{
    public class AttackPointCalculatorLinker
    {
        public IAttackPointCalculator Value { get; private set; }

        public AttackPointCalculatorLinker()
        {
            
        }
        public AttackPointCalculatorLinker(IAttackPointCalculator value)
        {
            Value = value; 
        }
    }
}