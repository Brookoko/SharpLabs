namespace App
{
    using System;
    using System.Linq;
    using ConsoleApp;
    using DependencyInjection;
    using Entities;
    using Environment;
    using Flow;

    public class StartOptions : Options
    {
        [Inject]
        public IEntityManager EntityManager { get; set; }

        [Inject]
        public World World { get; set; }

        [Inject]
        public EntityAttacking EntityAttacking { get; set; }
        
        public override string Id => "Start";

        public StartOptions()
        {
            AddOption("--attack #id1 #id2", parameters => Attack(parameters.Ints[0], parameters.Ints[1]));
            AddOption("--step", _ => Step());
            AddOption("--list", _ => PrintEntities());
            AddOption("--world", _ => PrintWorld());
        }
        
        private void Attack(int id1, int id2)
        {
            var attackingEntity = EntityManager.Entities.FirstOrDefault(entity => entity.Id == id1);
            if (attackingEntity == null)
            {
                Console.WriteLine($"No entity with id: {id1}");
                return;
            }
            var attackedEntity = EntityManager.Entities.FirstOrDefault(entity => entity.Id == id2);
            if (attackedEntity == null)
            {
                Console.WriteLine($"No entity with id: {id2}");
                return;
            }
            EntityAttacking.Dispatch(attackingEntity, attackedEntity);
        }
        
        private void Step()
        {
            EntityManager.UpdateEntities();
        }
        
        private void PrintEntities()
        {
            foreach (var entity in EntityManager.Entities)
            {
                PrintEntityInfo(entity);
            }
        }

        private void PrintEntityInfo(Entity entity)
        {
            Console.WriteLine($"{entity.Name} ({entity.Id}) Hp: {entity.Hp:F3}");
        }

        private void PrintWorld()
        {
            foreach (var position in World.Positions)
            {
                Console.WriteLine($"({position.x}, {position.y}) {position.type}");
            }
        }
    }
}