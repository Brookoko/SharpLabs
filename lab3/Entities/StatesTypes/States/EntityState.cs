namespace Entities.StatesTypes.States
{
    using SM.States;
    
    public abstract class EntityState : State
    {
        protected readonly Entity Entity;

        protected EntityState(Entity entity)
        {
            Entity = entity;
        }

        public abstract void Prepare();
    }
}