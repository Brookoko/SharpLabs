namespace Entities
{
    public abstract class Weapon : IOffensive
    {
        public abstract float Damage { get; }
        
        public abstract AttackEffect Effect { get; }
        
        public Damage Attack(Entity entity)
        {
            return new Damage
            {
                Amount = Damage,
                Effect = Effect,
            };
        }
    }
}