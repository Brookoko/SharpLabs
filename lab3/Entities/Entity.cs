namespace Entities
{
    using Environment;
    using Exceptions;

    public abstract class Entity : IHittable, IHealable, IHealer, IOffensive, IMovable, ICloneable<Entity>
    {
        public Hitbox Hitbox { get; }
        
        public Weapon Weapon { get; set; }
        
        public float Hp => Hitbox.Hp;

        public MovementType MovementType { get; set; }
        
        public Position Position { get; protected set; }
        
        public float Healing { get; set; }
        
        public Entity(Hitbox hitbox)
        {
            Hitbox = hitbox;
            hitbox.OnDamage += OnDamage;
            hitbox.OnDeath += OnDeath;
        }
        
        protected virtual void OnDamage()
        {
        }

        protected virtual void OnDeath()
        {
        }
        
        public void TakeDamage(Damage damage)
        {
            Hitbox.TakeDamage(damage);
        }
        
        public virtual void Heal(Entity entity)
        {
            throw new InvalidActionException("Cannot use heal on this character");
        }
        
        public void TakeHealing(float healing)
        {
            Hitbox.TakeHealing(healing);
        }
        
        public Damage Attack(Entity entity)
        {
            return Weapon?.Attack(entity) ?? Damage.NoDamage;
        }
        
        public void Move(Position position)
        {
            if (position.CanTraverse(this))
            {
                Position = position;
            }
        }
        
        public abstract Entity Clone();
    }
}