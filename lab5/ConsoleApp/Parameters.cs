namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Parameters
    {
        public string String => Strings.First();
        
        public int Int => Ints.First();
        
        public List<string> Strings { get; } = new List<string>();
        
        public List<int> Ints { get; } = new List<int>();
        
        public void AddParameter(string pattern, string parameter)
        {
            if (pattern.StartsWith("$"))
            {
                Strings.Add(parameter);
            }
            else if (pattern.StartsWith("#"))
            {
                if (int.TryParse(parameter, out var integer))
                {
                    Ints.Add(integer);
                }
            }
        }
    }
}