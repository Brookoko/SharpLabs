namespace Entities.Types
{
    using StatesTypes.SMTypes;
    
    public class Healer : StateDrivenEntity
    {
        public Healer(Hitbox hitbox, EntityStateMachine sm) : base(hitbox, sm)
        {
        }
        
        public override void Heal(Entity entity)
        {
            entity.TakeHealing(Healing);
        }
        
        public override Entity Clone()
        {
            return new Healer(Hitbox.Clone(), sm.Clone())
            {
                Name = Name,
                Weapon = Weapon,
                MovementType = MovementType,
                Position = Position,
                Healing = Healing,
            };
        }
    }
}