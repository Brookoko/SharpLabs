namespace Entities.Types
{
    using StatesTypes.SMTypes;

    public class Knight : StateDrivenEntity
    {
        public Knight(Hitbox hitbox, EntityStateMachine sm) : base(hitbox, sm)
        {
        }
    }
}