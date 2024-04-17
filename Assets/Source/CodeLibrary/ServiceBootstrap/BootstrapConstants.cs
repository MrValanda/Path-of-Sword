namespace Source.CodeLibrary.ServiceBootstrap
{
    public static class BootstrapConstants
    {
        public const string ParentName = "==== Bootstrappers ====";
        public const string MenuPath = "Bootstrapper/";
        public const string MenuScenesPath = "Bootstrapper/Scenes/";
        
        public const string GlobalServiceLocatorName = "==== ServiceLocator [Global] ====";
        
        public static string GetFullName(string sceneName) => $"ServiceLocator [Scene:{sceneName}]";
    }
}