namespace Entities
{
    public interface IHealable
    {
        void TakeHealing(float healing);
    }
    
    public interface IHealer
    {
        float Healing { get; }
        
        void Heal(Entity entity);
    }
}