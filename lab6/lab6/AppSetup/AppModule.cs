namespace AppSetup
{
    using AppContext;
    using Data;
    using DependencyInjection;
    using Flow;
    using lab6;

    public class AppModule : ModuleInstaller
    {
        public override IModuleInitializer LogicInitializer => new Initializer();
        
        public override string Name => "App";
        
        protected override void ExecuteAfterBindings()
        {
            InjectorBinder.Bind<ProjectsHolder>().To<ProjectsHolder>().ToSingleton();
            InjectorBinder.Bind<Queries>().To<Queries>().ToSingleton();
            InjectorBinder.Bind<Database>().To<Database>().ToSingleton();
            
            CommandBinder.Bind<StartApp>()
                .To<StartModules>()
                .To<SetupCommand>()
                .InSequence();
        }
        
        private class Initializer : IModuleInitializer
        {
            [Inject]
            public Database Database { get; set; }
            
            public void Prepare()
            {
            }
        }
    }
}