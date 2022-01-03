using System.Collections.Generic;

namespace Andy.AKOS
{
    public class Environment
    {
        private Dictionary<string, string> vars;

        public Environment() => Initialise();

        public void Initialise()
        {
            vars = new Dictionary<string, string>();
        }

        public void AddVariable(string name, string value)
        {
            if (vars.ContainsKey(name))
            {
                AKOS.Current.logger.Log($"ENVIRONMENT: ALREADY EXISTS; SET -> {name}", Logger.LogLevel.Warning);
                return;
            }
            vars.Add(name, value);
        }

        public void SetVariable(string name, string value)
        {
            if (!vars.ContainsKey(name))
            {
                AKOS.Current.logger.Log($"ENVIRONMENT: DOES NOT EXIST; SET -> {name}", Logger.LogLevel.Warning);
                return;
            }
            vars[name] = value;
        }

        public void RemoveVariable(string name)
        {
            if (!vars.ContainsKey(name))
            {
                AKOS.Current.logger.Log($"ENVIRONMENT: DOES NOT EXIST; REMOVE -> {name}", Logger.LogLevel.Warning);
                return;
            }

            vars.Remove(name);
        }
        public string GetVariable(string name)
        {
            if (!vars.ContainsKey(name))
            {
                AKOS.Current.logger.Log($"ENVIRONMENT: DOES NOT EXIST; GET -> {name}", Logger.LogLevel.Warning);
                return null;
            }

            return vars[name];
        }

        public bool HasVariable(string name) => vars.ContainsKey(name);
    }
}
