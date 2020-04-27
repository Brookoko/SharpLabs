namespace Entities.StatesTypes.SMTypes
{
    using System;
    using DependencyInjection;
    using SM;

    public class EntityStateMachine : StateMachine, ICloneable<EntityStateMachine>
    {
        [Inject]
        public IInjectionBinder Binder { get; set; }
        
        public virtual void Prepare(Entity entity)
        {
        }
        
        public EntityStateMachine Clone()
        {
            var sm = (EntityStateMachine) Activator.CreateInstance(GetType());
            sm.Binder = Binder;
            return sm;
        }
    }
}