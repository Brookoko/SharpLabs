namespace Civilization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConsoleOptionsState
    {
        public Options Name { get; set; }
        private Dictionary<string, Action<Parameters>> options = new Dictionary<string, Action<Parameters>>();
        private Dictionary<string, string> descriptions = new Dictionary<string, string>();
        protected ConsoleOptions sm;
        
        public ConsoleOptionsState(ConsoleOptions options)
        {
            sm = options;
        }
        
        public void AddOption(string option, Action<Parameters> action)
        {
            var parameters = option.Split(' ');
            var name = parameters[0];
            descriptions[name] = option;
            options[name] = action;
        }

        public void RunOption(string option)
        {
            var (name, param) = ParseString(option);
            if (name == null) return;
            if (options.TryGetValue(name, out var action))
            {
                action?.Invoke(param);
                return;
            }
            Console.WriteLine($"No option with name {name}");
        }

        private (string name, Parameters param) ParseString(string option)
        {
            option = option.Trim();
            var parameters = option.Split(' ');
            var name = parameters[0];
            if (!descriptions.ContainsKey(name))
            {
                Console.WriteLine($"No option with name {name}");
                return (null, null);
            }
            var patterns = descriptions[name].Split(' ');
            if (parameters.Length != patterns.Length)
            {
                Console.WriteLine("Insufficient number of parameters");
                return (null, null);
            }
            var param = CreateParameters(patterns, parameters);
            return (name, param);
        }

        private Parameters CreateParameters(string[] patterns, string[] parameters)
        {
            var param = new Parameters();
            for (var i = 1; i < patterns.Length; i++)
            {
                var paramString = patterns[i];
                if (paramString.StartsWith("$"))
                {
                    param.StringParam = parameters[i];
                    param.StringParameters.Add(parameters[i]);
                }
                else if (paramString.StartsWith("#"))
                {
                    if (int.TryParse(parameters[i], out var num))
                    {
                        param.IntParam = num;
                    }
                }
            }
            return param;
        }

        public void PrintOptions()
        {
            var option = options.Aggregate("\n", (current, pair) => current + $"{descriptions[pair.Key]}\n");
            Console.WriteLine(option);
        }

        public virtual void OnEnter()
        {
            PrintOptions();
        }

        protected void ChangeState(Options state)
        {
            sm.ChangeState(state);
        }
    }
}