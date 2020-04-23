namespace Entities
{
    using SM;

    public abstract class StateDrivenEntity : Entity
    {
        private StateMachine sm;
        
        public StateDrivenEntity(Hitbox hitbox, StateMachine sm) : base(hitbox)
        {
            this.sm = sm;
        }
        
        public void Update()
        {
            sm.Update();
        }
    }
}