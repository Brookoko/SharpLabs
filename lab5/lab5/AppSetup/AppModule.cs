namespace AppSetup
{
    using AppContext;
    using Data;
    using Flow;
    using Xml;

    public class AppModule : ModuleInstaller
    {
        public override string Name => "App";
        
        protected override void ExecuteAfterBindings()
        {
            InjectorBinder.Bind<ProjectsHolder>().To<ProjectsHolder>().ToSingleton();
            InjectorBinder.Bind<Queries>().To<Queries>().ToSingleton();
            
            InjectorBinder.Bind<XmlDataLoader>().To<XmlDataLoader>().ToSingleton();
            
            CommandBinder.Bind<StartApp>()
                .To<CleanUpCommand>()
                .To<SetupCommand>()
                .To<ConvertToXElementCommand>()
                .InSequence();
        }
    }
}