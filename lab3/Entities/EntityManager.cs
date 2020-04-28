namespace Entities
{
    using System.Collections.Generic;
    
    public interface IEntityManager
    {
        IEnumerable<Entity> Entities { get; }
        
        void Register(Entity entity);
        
        void UpdateEntities();
    }
    
    public class EntityManager : IEntityManager
    {
        public IEnumerable<Entity> Entities => entities;
        
        private readonly List<Entity> entities = new List<Entity>();
        
        public void Register(Entity entity)
        {
            entity.Id = entities.Count;
            entity.Prepare();
            entities.Add(entity);
            entity.Hitbox.OnDeath += () => Remove(entity);
        }
        
        public void UpdateEntities()
        {
            foreach (var entity in entities)
            {
                entity.Update();
            }
        }
        
        private void Remove(Entity entity)
        {
            entities.Remove(entity);
        }
    }
}