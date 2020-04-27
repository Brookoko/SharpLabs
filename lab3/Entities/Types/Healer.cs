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
    }
}