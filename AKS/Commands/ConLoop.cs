using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Andy.AKOS;
using Andy.AKOS.Arguments;
using Andy.AKOS.Component;

namespace AKS.Commands
{
    class ConLoop : IComponentBase
    {
        private string curPath = "";

        public void Initialise()
        {
            curPath = "C:\\";
            Directory.SetCurrentDirectory(curPath);
        }

        public void Run(object[] args)
        {
            while (true)
            {
                AKOS.Current.consoleManager.FlushConsoleInputStream();
                Console.Write(curPath + "> ");
                string input = Console.ReadLine().Trim();
                string callName = "";
                ArgumentParser.ArgumentsContainer arguments = AKOS.Current.argumentPasser.GetNewArgumentContainer();

                int spaceIndex = input.IndexOf(' ');
                if (spaceIndex == -1)
                    callName = input;
                else
                {
                    callName = input[..spaceIndex];
                    arguments.Populate(input[(spaceIndex + 1)..]);
                }

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
