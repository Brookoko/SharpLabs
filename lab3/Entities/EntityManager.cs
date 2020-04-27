namespace Entities
{
    using System.Collections.Generic;
    
    public interface IEntityManager
    {
        void Register(Entity entity);
    }
    
    public class EntityManager : IEntityManager
    {
        private readonly List<Entity> entities = new List<Entity>();
        
        public void Register(Entity entity)
        {
            entities.Add(entity);
            entity.Hitbox.OnDeath += () => Remove(entity);
        }
        
        public void Remove(Entity entity)
        {
            entities.Remove(entity);
        }
    }
}