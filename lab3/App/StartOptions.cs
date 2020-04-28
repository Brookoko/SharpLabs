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

        [Inject]
        public EntityHealing EntityHealing { get; set; }

        [Inject]
        public EntityMoving EntityMoving { get; set; }
        
        public override string Id => "Start";

        public StartOptions()
        {
            AddOption("--attack #id1 #id2", parameters => Attack(parameters.Ints[0], parameters.Ints[1]));
            AddOption("--heal #id1 #id2", parameters => Heal(parameters.Ints[0], parameters.Ints[1]));
            AddOption("--move #id1 #x #y", parameters => Move(parameters.Ints[0], parameters.Ints[1], parameters.Ints[2]));
            AddOption("--step", _ => Step());
            AddOption("--list", _ => PrintEntities());
            AddOption("--world", _ => PrintWorld());
        }
        
        private void Attack(int id1, int id2)
        {
            if (TryGetEntity(id1, out var attackingEntity) && TryGetEntity(id2, out var attackedEntity))
            {
                EntityAttacking.Dispatch(attackingEntity, attackedEntity);
            }
        }
        
        private void Heal(int id1, int id2)
        {
            if (TryGetEntity(id1, out var healingEntity) && TryGetEntity(id2, out var healedEntity))
            {
                EntityHealing.Dispatch(healingEntity, healedEntity);
            }
        }

        private void Move(int id, int x, int y)
        {
            if (TryGetEntity(id, out var entity) && TryGetPosition(x, y, out var position))
            {
                if (!position.CanTraverse(entity))
                {
                    Console.WriteLine($"Entity cannot move to {position.ToString()}");
                }
                EntityMoving.Dispatch(entity, position);
            }
        }
        
        private bool TryGetEntity(int id, out Entity entity)
        {
            entity = EntityManager.Entities.FirstOrDefault(e => e.Id == id);
            if (entity == null)
            {
                Console.WriteLine($"No entity with id: {id}");
                return false;
            }
            return true;
        }

        private bool TryGetPosition(int x, int y, out Position position)
        {
            if (World.Positions.Any(p => p.x == x && p.y == y))
            {
                position = World.Positions.First(p => p.x == x && p.y == y);
                return true;
            }
            position = default;
            return false;
        }
        
        private void Step()
        {
            EntityManager.UpdateEntities();
        }
        
        private void PrintEntities()
        {
            foreach (var entity in EntityManager.Entities)
            {
                Console.WriteLine($"{entity.Name} ({entity.Id}) Pos: {entity.Position.ToString()} Hp: {entity.Hp:F3}");
            }
        }
        
        private void PrintWorld()
        {
            foreach (var position in World.Positions)
            {
                Console.WriteLine(position.ToString());
            }
        }
    }
}