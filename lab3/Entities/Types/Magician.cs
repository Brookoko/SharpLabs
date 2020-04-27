namespace Entities.Types
{
    using StatesTypes.SMTypes;

    public class Magician : StateDrivenEntity
    {
        public Magician(Hitbox hitbox, EntityStateMachine sm) : base(hitbox, sm)
        {
        }
    }
}