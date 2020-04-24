namespace Entities
{
    using SM;
    using StatesTypes.SMTypes;

    public abstract class StateDrivenEntity : Entity
    {
        private StateMachine sm;
        
        public StateDrivenEntity(Hitbox hitbox, EntityStateMachine sm) : base(hitbox)
        {
            this.sm = sm;
            sm.Prepare(this);
        }
        
        public void Update()
        {
            sm.Update();
        }
    }
}