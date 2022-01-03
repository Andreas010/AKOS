using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration.Ini;

namespace Andy.AKOS.Config
{
    public class ConfigLoaders
    {
        public ConfigLoaders()
        {
            AkosConfigManager = new AkosConfig();
        }

        public AkosConfig AkosConfigManager { get; private set; }

        public class AkosConfig
        {
            public AkosConfigTable Get(string text) => Get(text.Split('\n'));
            public AkosConfigTable Get(string[] lines)
            {
                AkosConfigTable table = new();
                List<AkosConfigValue> values = new();

                for(int i = 0; i < lines.Length; i++)
                {
                    string curLine = lines[i].Trim();
                    if (!curLine.Contains(':') || !curLine.Contains('=')) continue;
                    int colonIndex = curLine.IndexOf(':');
                    int equalsIndex = curLine.IndexOf('=');

                    if (colonIndex > equalsIndex) continue;

                    string name  = curLine[..colonIndex].Trim();
                    string type  = curLine[(colonIndex+1)..equalsIndex].Trim();
                    string value = curLine[(equalsIndex+1)..].Trim();

                    Console.WriteLine($"\"{name}|{type}|{value}\"");
                }

                return table;
            }

            public struct AkosConfigTable
            {
                public AkosConfigValue[] values;
            }

            public struct AkosConfigValue
            {
                public string name;
                public string type;
                public string value;
            }
        }
    }
}
