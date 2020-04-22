namespace Civilization
{
    using System;

    public class StartMenu : ConsoleOptionsState
    {
        public StartMenu(ConsoleOptions options) : base(options)
        {
            Name = Options.Start;
            AddOption("--nations", param => ChangeState(Options.Nation));
            AddOption("--terr", param => ChangeState(Options.Territories));
            AddOption("--exit", param => Environment.Exit(0));
        }
    }
}