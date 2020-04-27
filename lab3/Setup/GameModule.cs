namespace Setup
{
    using AppContext;
    using Entities;
    using Flow;
    using Flow.Commands;

    public class GameModule : ModuleInstaller
    {
        public override string Name => "Game";
        
        protected override void ExecuteAfterBindings()
        {
            InjectorBinder.Bind<EntityFactory>().To<EntityFactory>().ToSingleton();
            InjectorBinder.Bind<IEntityManager>().To<EntityManager>().ToSingleton();
            
            InjectorBinder.Bind<EntityMoving>().ToSingleton();
            InjectorBinder.Bind<EntityAttacking>().ToSingleton();
            InjectorBinder.Bind<EntityHealing>().ToSingleton();
            
            CommandBinder.Bind<StartApp>()
                .To<SetupEntityVariantsCommand>()
                .To<CreateRandomEntitiesCommand>()
                .InSequence();
        }
    }
}
