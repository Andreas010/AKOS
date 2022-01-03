using Andy.AKOS.Arguments;
using Andy.AKOS.Component;
using Andy.AKOS.Config;

namespace Andy.AKOS
{
    public class AKOS
    {
        public const string version = "v1.0";

#if DEBUG
        public const string state = "Debug";
#else
        public const string state = "Release";
#endif

        public AKOS() => Initialise();

        public ConsoleManager consoleManager;
        public ComponentSystem componentSystem;
        public DllLoader dllLoader;
        public Logger logger;
        public ArgumentPasser argumentPasser;
        public Environment environment;
        public LoadingVisualiser loadingVisualiser;
        public ConfigLoaders configLoaders;

        public static AKOS Current;

        public static void P() => _ = new AKOS();

        public void Initialise()
        {
            Current = this;
            logger = new();
            consoleManager = new();
            argumentPasser = new();
            componentSystem = new();
            environment = new();
            loadingVisualiser = new();
            configLoaders = new();
            componentSystem.Initialise();

            dllLoader = new();
            dllLoader.Initialise();
        }
    }
}
