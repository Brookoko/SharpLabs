namespace Entities
{
    using SM;
    using StatesTypes.SMTypes;

    public class StateDrivenEntity : Entity
    {
        protected EntityStateMachine sm;
        
        public StateDrivenEntity(Hitbox hitbox, EntityStateMachine sm) : base(hitbox)
        {
            this.sm = sm;
        }
        
        public override void Prepare()
        {
            sm.Prepare(this);
            sm.Start();
        }
        
        public override void Update()
        {
            sm.Update();
        }
        
        public override Entity Clone()
        {
            return new StateDrivenEntity(Hitbox.Clone(), sm.Clone())
            {
                Name = Name,
                Weapon = Weapon,
                MovementType = MovementType,
                Position = Position
            };
        }
    }
}