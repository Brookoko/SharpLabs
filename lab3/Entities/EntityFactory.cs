namespace Entities
{
    using System.Collections.Generic;
    
    public class EntityFactory
    {
        private readonly Dictionary<string, Entity> entities = new Dictionary<string, Entity>();
        
        public void Register(string name, Entity entity)
        {
            entities[name] = entity;
        }
        
        public IEnumerable<string> Variants => entities.Keys;
        
        public Entity Create(string name)
        {
            var proto = entities[name];
            return proto.Clone();
        }
    }
}