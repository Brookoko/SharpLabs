namespace Civilization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Territories;
    using Worlds;

    public class TerritoriesMenu : ConsoleOptionsState
    {
        private readonly World world;
        private List<Territory> Territories => world.Territories;
        
        private readonly Random random = new Random();
        
        public TerritoriesMenu(ConsoleOptions options) : base(options)
        {
            Name = Options.Territories;
            AddOption("--available", param => PrintAvailable());
            AddOption("--create $type$", param => CreateTerritory(param.StringParam));
            AddOption("--list", param => PrintTerritories());
            AddOption("--random #count#", param => CreateRandom(param.IntParam));
            AddOption("--back", param => ChangeState(Options.Start));
        }
        
        public TerritoriesMenu(ConsoleOptions sm, World world) : this(sm)
        {
            this.world = world;
        }

        private void PrintAvailable()
        {
            var line = TerritoryFactory.Instance.Keys().Aggregate("", (current, key) => current + $"{key}\n");
            Console.WriteLine(line);
        }

        private void CreateTerritory(string typeName)
        {
            var terr = TerritoryFactory.Instance.Create(typeName);
            if (terr != null)
            {
                Territories.Add(terr);
            }
            else
            {
                Console.WriteLine($"Cannot create territory with name {typeName}");
            }
        }

        private void CreateRandom(int count)
        {
            Territories.AddRange(TerritoryFactory.Instance.CreateRandom(count));
        }

        private void PrintTerritories()
        {
            var line = Territories.Aggregate("", (current, ter) => current + $"{ter.Id} ({ter.GetType().Name})\n");
            Console.WriteLine(line);
        }
    }
}