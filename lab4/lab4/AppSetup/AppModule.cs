namespace AppSetup
{
    using AppContext;

    public class AppModule : ModuleInstaller
    {
        public override string Name => "App";
        
        protected override void ExecuteAfterBindings()
        {
            InjectorBinder.Bind<StartApp>().ToSingleton();
        }
    }
}