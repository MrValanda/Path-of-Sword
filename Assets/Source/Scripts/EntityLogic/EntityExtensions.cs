namespace Source.Scripts.EntityLogic
{
    public static class EntityExtensions
    {
        public static T Get<T>(this Entity entity) where T : class
        {
            return entity.ComponentContainerMonoLinker.ComponentsContainer.GetComponent<T>();
        }

        public static void Add<T>(this Entity entity, T addedItem) where T : class
        {
            entity.ComponentContainerMonoLinker.ComponentsContainer.AddComponent(addedItem);
        }

        public static T AddOrGet<T>(this Entity entity) where T : class, new()
        {
           return entity.ComponentContainerMonoLinker.ComponentsContainer.AddOrGetComponent<T>();
        }
    }
}