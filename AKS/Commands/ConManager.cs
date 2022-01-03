using System;
using System.IO;
using Andy.AKOS;
using Andy.AKOS.Component;

namespace AKS.Commands
{
    class ConManager : IComponentBase
    {
        private string curPath = "";

        public void Initialise()
        {
            curPath = "C:\\";
            Directory.SetCurrentDirectory(curPath);
        }

        public void Run(object[] args)
        {
            AKOS.Current.consoleManager.MoveBuffer(10);
            Console.WriteLine();

            while(true)
            {
                AKOS.Current.consoleManager.FlushConsoleInputStream();
                Console.Write(curPath + "> ");
                /*
                string path = Console.ReadLine();
                path = Path.GetFullPath(path);
                if(Path.GetPathRoot(path) != Path.GetPathRoot(curPath))
                {
                    Directory.SetCurrentDirectory(Path.GetPathRoot(path));
                    curPath = Directory.GetCurrentDirectory();
                    continue;
                }


                if(Directory.Exists(path))
                {
                    curPath = path;
                    Directory.SetCurrentDirectory(curPath);
                }
                */
            }
        }
    }
}