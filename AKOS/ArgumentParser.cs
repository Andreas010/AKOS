﻿using System.Collections.Generic;

namespace Andy.AKOS.Arguments
{
    public class ArgumentParser
    {
        public ArgumentsContainer GetNewArgumentContainer() => new();

        public class ArgumentsContainer
        {
            private List<BooleanArgument> booleanArgs;
            private List<ValueArgument> valueArgs;

            public ArgumentsContainer() => Initialise();

            public void Initialise()
            {
                booleanArgs = new List<BooleanArgument>();
                valueArgs = new List<ValueArgument>();
            }

            public bool HasArgument(string name, bool value, bool ignoreCase = false)
            {
                if(!value)
                {
                    for (int i = 0; i < booleanArgs.Count; i++)
                    {
                        if ((ignoreCase ? booleanArgs[i].name.ToLower() : booleanArgs[i].name) == (ignoreCase ? name.ToLower() : name))
                            return true;
                    }
                } else
                {
                    for (int i = 0; i < valueArgs.Count; i++)
                    {
                        if ((ignoreCase ? valueArgs[i].name.ToLower() : valueArgs[i].name) == (ignoreCase ? name.ToLower() : name))
                            return true;
                    }
                }
                
                return false;
            }

            public bool GetBooleanArgument(string name)
            {
                for (int i = 0; i < booleanArgs.Count; i++)
                {
                    if (booleanArgs[i].name == name)
                        return true;
                }

                return false;
            }

            public string GetValueArgument(string name)
            {
                for (int i = 0; i < valueArgs.Count; i++)
                {
                    if (valueArgs[i].name == name)
                        return valueArgs[i].value;
                }

                return null;
            }

            public ArgumentsContainer Populate(string argsString)
            {
                Initialise();

                string[] args = argsString.Split(' ');

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("--"))
                    {
                        booleanArgs.Add(new BooleanArgument()
                        {
                            name = args[i].Remove(0, 2)
                        });
                    }
                    else if (args[i].StartsWith("-"))
                    {
                        char equalsSign = '=';
                        if (!args[i].Contains(equalsSign))
                        {
                            equalsSign = ':';
                            if(!args[i].Contains(equalsSign))
                                continue;
                        }

                        int equalsSignPos = args[i].IndexOf(equalsSign);

                        valueArgs.Add(new ValueArgument()
                        {
                            name = args[i][1..equalsSignPos],
                            value = args[i].Substring(equalsSignPos + 1, args[i].Length - equalsSignPos - 1)
                        });
                    }
                }

                return this;
            }
        }

        public struct BooleanArgument
        {
            public string name;
        }

        public struct ValueArgument
        {
            public string name;
            public string value;
        }
    }
}
