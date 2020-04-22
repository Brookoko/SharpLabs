namespace ConsoleUtils
{
    using System;
    using Types;

    public class ConsoleProgram : IProgram
    {
        protected IOptions options;
        private bool shouldProcessInput;
        
        public virtual void Init()
        {
            PrintOptions(options);
            shouldProcessInput = true;
        }

        public void ProcessInput()
        {
            while (shouldProcessInput)
            {
                var option = Console.ReadLine();
                if (!options.ValidateInput(option))
                {
                    Console.WriteLine($"No option for input: {option}. Try next one");
                    PrintOptions(options);
                    continue;
                }
                options.RunOption(option);
            }
        }
        
        public void ChangeOptions(IOptions options)
        {
            this.options = options;
            PrintOptions(this.options);
        }

        protected void StopProcessingInput()
        {
            shouldProcessInput = false;
        }
        
        private void PrintOptions(IOptions options)
        {
            foreach (var option in options.OptionsList())
            {
                Console.WriteLine($"{option}");
            }
            Console.WriteLine();
        }
        
        public void Exit()
        {
            Console.WriteLine("Program is completed. Bye");
        }
    }
}