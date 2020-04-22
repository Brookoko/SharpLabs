namespace BallDefence.Injection
{
    using Commands;
    
    public abstract class Context
    {
        private IInjectionBinder injectionBinder;
        private ICommandInjector commandInjector;
        
        public IInjectionBinder InjectionBinderBinder
        {
            get => injectionBinder ?? new InjectionBinder();
            private set => injectionBinder = value;
        }
        
        public ICommandInjector CommandInjector
        {
            get => commandInjector ?? new CommandInjector();
            private set => commandInjector = value;
        }
        
        public void Start()
        {
            Prepare();
            ApplyBindings();
            Launch();
        }
        
        protected virtual void Prepare()
        {
            InjectionBinderBinder = new InjectionBinder();
            InjectionBinderBinder.Bind<IInjectionBinder>().ToInstance(InjectionBinderBinder);
            CommandInjector = new CommandInjector {InjectionBinder = InjectionBinderBinder};
            InjectionBinderBinder.Bind<ICommandInjector>().ToInstance(CommandInjector);
        }
        
        protected virtual void ApplyBindings()
        {
        }
        
        protected abstract void Launch();
    }
}