using System;
using System.Collections.Generic;
using Andy.AKOS;
using Andy.AKOS.Component;
using Andy.AKOS.Arguments;

namespace AKS.Commands
{
    class ConManager : IComponentBase
    {
        public Dictionary<string, IComponentBase> commands;

        public void Initialise()
        {
            /// TODO: Load commands
            commands = new();
        }

        public void Run(object args)
        {
            AKOS.Current.consoleManager.MoveBuffer(10);
            Console.WriteLine();

            AKOS.Current.componentSystem.RunComponent("conLop", this);

            
        }
    }
}