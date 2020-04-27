namespace Entities
{
    using SM;
    using StatesTypes.SMTypes;

    public class StateDrivenEntity : Entity
    {
        private EntityStateMachine sm;
        
        public StateDrivenEntity(Hitbox hitbox, EntityStateMachine sm) : base(hitbox)
        {
            this.sm = sm;
            sm.Prepare(this);
            sm.Start();
        }
        
        public void Update()
        {
            sm.Update();
        }
        
        public override Entity Clone()
        {
            return new StateDrivenEntity(Hitbox, sm.Clone())
            {
                Weapon = Weapon,
                MovementType = MovementType,
                Position = Position,
                Healing = Healing,
            };
        }
    }
}