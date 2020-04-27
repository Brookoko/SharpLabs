namespace Entities.Types
{
    using StatesTypes.SMTypes;

    public class Orc : StateDrivenEntity
    {
        public Orc(Hitbox hitbox, EntityStateMachine sm) : base(hitbox, sm)
        {
        }
    }
}