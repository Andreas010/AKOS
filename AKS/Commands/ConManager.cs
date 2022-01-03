using System;
using System.IO;
using Andy.AKOS;
using Andy.AKOS.Component;
using Andy.AKOS.Arguments;

namespace AKS.Commands
{
    class ConManager : IComponentBase
    {
        public void Initialise()
        {
            /// TODO: Load commands
        }

        public void Run(object[] args)
        {
            AKOS.Current.consoleManager.MoveBuffer(10);
            Console.WriteLine();

            AKOS.Current.componentSystem.RunComponent("conLop", null);
        }
    }
}