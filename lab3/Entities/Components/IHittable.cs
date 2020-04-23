namespace Entities
{
    public interface IHittable
    {
        float Hp { get; }

        void TakeDamage(Damage damage);
    }
}