namespace AppContext
{
    using BallDefence.Injection;
    using BallDefence.Injection.Commands;
    
    public class StartModules : Command
    {
        [Inject]
        public ModuleInitializerHolder ModuleInitializerHolder { get; set; }

        [Inject]
        public InjectionBinder Injector { get; set; }
        
        public override void Execute()
        {
            foreach (var initializer in ModuleInitializerHolder.Initializers)
            {
                Injector.Inject(initializer);
                initializer.Prepare();
            }
        }
    }
}