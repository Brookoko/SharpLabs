namespace AppSetup
{
    using AppContext;
    using Data;
    using lab4.Flow;

    public class AppModule : ModuleInstaller
    {
        public override string Name => "App";
        
        protected override void ExecuteAfterBindings()
        {
            InjectorBinder.Bind<ProjectsHolder>().To<ProjectsHolder>().ToSingleton();
            InjectorBinder.Bind<Queries>().To<Queries>().ToSingleton();
            
            CommandBinder.Bind<StartApp>()
                .To<SetupCommand>()
                .InSequence();
        }
    }
}