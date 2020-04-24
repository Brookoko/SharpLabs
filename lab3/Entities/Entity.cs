namespace Entities
{
    using Environment;

    public abstract class Entity : IHittable, IHealable, IHealer, IOffensive, IMovable
    {
        public Hitbox Hitbox { get; }
        
        public Weapon Weapon { get; set; }
        
        public float Hp => Hitbox.Hp;

        public MovementType MovementType { get; }
        
        public Position Position { get; private set; }
        
        public abstract float Healing { get; set; }
        
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
        
        
        public abstract void Heal(Entity entity);
        
        public void TakeHealing(float healing)
        {
            Hitbox.TakeHealing(healing);
        }
        
        public Damage Attack(Entity entity)
        {
            return Weapon.Attack(entity);
        }
        
        public void Move(Position position)
        {
            if (position.CanTraverse(this))
            {
                Position = position;
            }
        }
    }
}