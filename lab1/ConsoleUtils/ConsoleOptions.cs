namespace Civilization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ConsoleOptions
    {
        private ConsoleOptionsState currentState;
        private List<ConsoleOptionsState> states = new List<ConsoleOptionsState>();
        
        public void RegisterState(ConsoleOptionsState state)
        {
            states.Add(state);
        }

        public void ChangeState(Options type)
        {
            var state = GetState(type);
            if (state != null)
            {
                currentState = state;
                currentState.OnEnter();
                return;
            }
            Console.WriteLine($"No state of type {type}");
        }

        public void RunOption(string name)
        {
            currentState.RunOption(name);
        }

        public void PrintOptions()
        {
            currentState.PrintOptions();
        }

        public ConsoleOptionsState GetState(Options type)
        {
            return states.FirstOrDefault(s => s.Name == type);
        }
    }
}