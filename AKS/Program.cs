using System;
using Andy.AKOS;
using Andy.AKOS.Component;
using Andy.AKOS.Config;
using Andy.AKOS.Arguments;
using System.Threading;
using System.IO;

namespace AKS
{
    class Program
    {
        static DateTime startTime;

        static void Main()
        {
            startTime = DateTime.Now;

            try
            {
                Console.SetWindowSize(120, 30);
                Console.SetBufferSize(120, 30);
            }
            catch
            {
                Console.SetWindowSize(120, 30);
                Console.SetBufferSize(120, 30);
            }

            Boot();
            Load();
            Start();
            End();

            Console.ReadKey(true);
        }

        static void Boot()
        {
            /*
             * Logo
             * Load assemblys
             * Instantiate Classes
             * Prepare Env Variables
             * [NOTIFY USER] Boot completed
             */

#if !DEBUG
            Logo();
#endif
            AKOS.P();
#if DEBUG
            AKOS.Current.environment.AddVariable("p_debug", "true");
#else
            AKOS.Current.environment.AddVariable("p_debug", "false");
#endif
            AKOS.Current.environment.AddVariable("akos_version", AKOS.version);

            AKOS.Current.logger.Log($"Boot Completed [{DateTime.Now - startTime}]", Logger.LogLevel.Important);
        }

        static void Logo()
        {
            string[] logo = 
            {
                " ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒ ",
                "▒▒▒▒▒▒▒▒▒▒▒████████▒▒▒██▒▒▒██████████▒▒▒▒▒▒▒▒▒",
                "▒▒▒▒▒▒▒▒▒▒███▓▓▓▓██▒▒██▓▒██▓▓▓▓▓▓▓▓▓▓██▒▒▒▒▒▒▒",
                "▒▒▒▒▒▒▒▒▒███▒▒▒▒▒██▒██▓▒▒██▒▒▒▒▒▒▒▒▒▒▓▓▒▒▒▒▒▒▒",
                "▒▒▒▒▒▒▒▒█████████████▓▒▒▒▓▓██████████▒▒▒▒▒▒▒▒▒",
                "▒▒▒▒▒▒▒███▓▓▓▓▓▓▓██▓██▒▒▒▒▒▓▓▓▓▓▓▓▓▓▓██▒▒▒▒▒▒▒",
                "▒▒▒▒▒▒███▓▒▒▒▒▒▒▒██▒▓██▒▒▒▒▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒▒",
                "▒▒▒▒▒███▓▒▒▒▒▒▒▒▒██▒▒▓██▒▒▒▒▒▒▒▒▒▒▒▒▒██▒▒▒▒▒▒▒",
                "▒▒▒▒███▓▒▒▒▒▒▒▒▒▒██▒▒▒▓██▒███████████▓▓▒▒▒▒▒▒▒",
                " ▒▒▒▓▓▓▒▒▒▒▒▒▒▒▒▒▓▓▒▒▒▒▓▓▒▓▓▓▓▓▓▓▓▓▓▓▒▒▒▒▒▒▒▒ "
            };

            for(int x = 0; x < Console.WindowWidth-1; x++)
            {
                for (int y = 0; y < Console.WindowHeight-1; y++)
                {
                    float uvX = (float)x / Console.WindowWidth;
                    float uvY = (float)y / Console.WindowHeight;

                    Console.SetCursorPosition(x, y);
                    char c = logo[(int)Math.Floor(uvY * logo.Length)][(int)Math.Floor(uvX * logo[0].Length)];

                    if (c == '▒')
                    {
                        Console.ForegroundColor = (x + y) % 2 == 0 ? ConsoleColor.Gray : ConsoleColor.Black;
                        Console.BackgroundColor = (x + y) % 2 == 0 ? ConsoleColor.Black : ConsoleColor.Gray;
                    }
                    else if (c == '▓')
                    {
                        Console.ForegroundColor = (x + y) % 2 == 0 ? ConsoleColor.DarkGray : ConsoleColor.Black;
                        Console.BackgroundColor = (x + y) % 2 == 0 ? ConsoleColor.Black : ConsoleColor.DarkGray;
                    }
                    else if (c == '█')
                    {
                        Console.ForegroundColor = (x + y) % 2 == 0 ? ConsoleColor.Black : ConsoleColor.White;
                        Console.BackgroundColor = (x + y) % 2 == 0 ? ConsoleColor.White : ConsoleColor.Black;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.BackgroundColor = ConsoleColor.Black;
                    }

                    Console.Write(c);
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;

            AKOS.Current.consoleManager.MoveBuffer(100);
        }

        static void Load()
        {
            /*
             * Register Basic Components
             * Load File System
             * [NOTIFY USER] Registered System
             * Check for external content
             * - Load Dlls
             * - Register & Initialise
             * - [NOTIFY USER] Loaded Dll "name"
             * Register Commands
             * Add Translations
             * [NOTIFY USER] Finished loading
             */

            /// TODO: Register Basic Components
            AKOS.Current.environment.AddVariable("p_path", InstanceField.programPath);
            AKOS.Current.environment.AddVariable("p_path_mod", InstanceField.programPath + "/modules/");

            AKOS.Current.logger.Log("Registered Systems", Logger.LogLevel.Important);

            string path = AKOS.Current.environment.GetVariable("p_path_mod");

            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string[] dirs = Directory.GetDirectories(path);

            for(int i = 0; i < dirs.Length; i++)
            {
                if(File.Exists(dirs[i] + "/module.json"))
                {
                    DllAkosPackage dll = Newtonsoft.Json.JsonConvert.DeserializeObject<DllAkosPackage>(File.ReadAllText(dirs[i] + "/module.json"));

                    if (dll == null)
                        continue;

                    AKOS.Current.dllLoader.LoadDll(dirs[i] + "/" + dll.filePath, dll.name);

                    AKOS.Current.logger.Log($"LOADED: {dll.name} ({Path.GetFileName(dll.filePath)}) | v{dll.version}");

                    if (AKOS.Current.dllLoader.HasDll(dll.name))
                        AKOS.Current.dllLoader.InitialiseDll(dll.name);
                }
            }

            /// TODO: Register Commands

            AKOS.Current.componentSystem.AddComponent("conMgr", new Commands.ConManager());
            // AKOS.Current.componentSystem.AddComponent("conLop", null);

            /// IDEA: Add Translations
            
            AKOS.Current.logger.Log("Finished Loading", Logger.LogLevel.Important);
        }

        static void Start()
        {

            /*
             * Ask user for Login
             * Run "conMgr"
             * [/] Run "cmdLop"
             * [/][/] Ask for Input
             * [/][/] Understand Input & Execute
             */

            AKOS.Current.componentSystem.RunComponent("conMgr", null);
        }

        static void End()
        {
            AKOS.Current.consoleManager.MoveBuffer(10);
            AKOS.Current.consoleManager.WriteSlow("Shutting Down...", 100);
            System.Environment.Exit(0);
        }
    }
}