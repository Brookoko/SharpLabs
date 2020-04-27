namespace App
{
    using ConsoleApp;
    using DependencyInjection;

    public class App : ConsoleProgram
    {
        private IInjectionBinder binder;
        
        public App(IInjectionBinder binder)
        {
            this.binder = binder;
            var start = (StartOptions) binder.Inject(new StartOptions());
            start.AddOption("--exit", _ => StopProcessingInput());
            Options = start;
        }
    }
}