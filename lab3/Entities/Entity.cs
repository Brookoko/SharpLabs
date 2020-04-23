namespace Entities
{
    using Environment;

    public abstract class Entity : IHittable, IHealable, IOffensive, IMovable
    {
        public Hitbox Hitbox { get; }
        
        public Weapon Weapon { get; set; }
        
        public float Hp => Hitbox.Hp;

        public MovementType MovementType { get; }
        
        public Position Position { get; private set; }
        
        public Entity(Hitbox hitbox)
        {
            Hitbox = hitbox;
            hitbox.OnDamage += OnDamage;
            hitbox.OnDeath += OnDeath;
        }
        
        protected abstract void OnDamage();

        protected abstract void OnDeath();
        
        public void TakeDamage(Damage damage)
        {
            Hitbox.TakeDamage(damage);
        }
        
        public void Heal(float healing)
        {
            Hitbox.Heal(healing);
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