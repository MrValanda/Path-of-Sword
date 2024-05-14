namespace Source.Scripts.EntityLogic
{
    public static class EntityExtensions
    {
        public static T Get<T>(this Entity entity) where T : class
        {
            return entity.ComponentContainerMonoLinker.ComponentsContainer.GetComponent<T>();
        }

        public static bool Contains<T>(this Entity entity) where T : class
        {
            return entity.ComponentContainerMonoLinker.ComponentsContainer.ContainsComponent<T>();
        }

        public static bool TryGet<T>(this Entity entity, out T component) where T : class
        {
            return entity.ComponentContainerMonoLinker.ComponentsContainer.TryGetComponent(out component);
        }

        public static void Add<T>(this Entity entity, T addedItem) where T : class
        {
            entity.ComponentContainerMonoLinker.ComponentsContainer.AddComponent(addedItem);
        }

        public static void Remove<T>(this Entity entity) where T : class
        {
            entity.ComponentContainerMonoLinker.ComponentsContainer.RemoveComponent<T>();
        }

        public static T AddOrGet<T>(this Entity entity) where T : class, new()
        {
            return entity.ComponentContainerMonoLinker.ComponentsContainer.AddOrGetComponent<T>();
        }
    }
}