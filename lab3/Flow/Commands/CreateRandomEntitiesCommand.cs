namespace Flow.Commands
{
    using System;
    using System.Linq;
    using DependencyInjection;
    using DependencyInjection.Commands;
    using Entities;
    
    public class CreateRandomEntitiesCommand : Command
    {
        [Inject]
        public EntityFactory EntityFactory { get; set; }

        [Inject]
        public IEntityManager EntityManager { get; set; }
        
        public override void Execute()
        {
            var variants = EntityFactory.Variants.ToList();
            var random = new Random();
            var count = random.Next(5, 15);
            for (var i = 0; i < count; i++)
            {
                var index = random.Next(variants.Count);
                var entity = EntityFactory.Create(variants[index]);
                EntityManager.Register(entity);
            }
        }
    }
}