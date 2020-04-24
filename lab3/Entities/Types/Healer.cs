namespace Entities.Types
{
    public class Healer : Entity
    {
        public Healer(Hitbox hitbox) : base(hitbox)
        {
        }
        
        public override float Healing { get;  set; }
        
        public override void Heal(Entity entity)
        {
            entity.TakeHealing(Healing);
        }
    }
}