namespace Source.Scripts.Interfaces
{
    public interface IFactory<out TFactoryValue, in TFactoryType>
    {
        public TFactoryValue GetFactoryValue(TFactoryType factoryType);
    }
}