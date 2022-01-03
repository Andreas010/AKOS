using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andy.AKOS
{
    public class LoadingVisualiser
    {
        Dictionary<string, Loader> loaders;

        public LoadingVisualiser() => Initialise();

        public void Initialise()
        {
            loaders = new();
        }

        public void SetupNewLoad(string name, int style, int maxValue)
        {
            if(loaders.ContainsKey(name))
            {
                AKOS.Current.logger.Log($"LOADING_VISUALISER: ALREADY EXISTS; SETUP -> {name}", Logger.LogLevel.Warning);
                return;
            }

            loaders.Add(name, new Loader()
            {
                maxValue = maxValue,
                value = 0,
                style = style
            });
        }

        public void SetLoadValue(string name, int value)
        {
            if (!loaders.ContainsKey(name))
            {
                AKOS.Current.logger.Log($"LOADING_VISUALISER: DOES NOT EXIST; SET -> {name}", Logger.LogLevel.Warning);
                return;
            }

            loaders[name].value = value;
        }

        public int GetLoadValue(string name)
        {
            if (loaders.ContainsKey(name))
            {
                AKOS.Current.logger.Log($"LOADING_VISUALISER: DOES NOT EXIST; GET -> {name}", Logger.LogLevel.Warning);
                return -1;
            }

            return loaders[name].value;
        }

        public void DisplayLoad(string name)
        {
            if (!loaders.ContainsKey(name))
            {
                AKOS.Current.logger.Log($"LOADING_VISUALISER: DOES NOT EXIST; DISPLAY -> {name}", Logger.LogLevel.Warning);
                return;
            }

            if (loaders[name].style == 0)
            {
                AKOS.Current.logger.Log($"LOADING_VISUALISER: DOES NOT EXIST; DISPLAY -> {name}", Logger.LogLevel.Warning);
                return;
            }

            /// TODO: Update Value
            
        }

        public void RemoveLoad(string name)
        {
            if (!loaders.ContainsKey(name))
            {
                AKOS.Current.logger.Log($"LOADING_VISUALISER: DOES NOT EXIST; REMOVE -> {name}", Logger.LogLevel.Warning);
                return;
            }

            loaders.Remove(name);
        }

        public bool HasLoad(string name) => loaders.ContainsKey(name);

        class Loader
        {
            public int style;
            public int maxValue;
            public int value;
        }
    }
}