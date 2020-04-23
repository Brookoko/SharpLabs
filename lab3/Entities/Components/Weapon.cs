namespace Entities
{
    public abstract class Weapon : IOffensive
    {
        public float Damage { get; }
        
        public AttackEffect Effect { get; }
        
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