namespace Civilization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Nations;
    using Worlds;

    public class NationMenu : ConsoleOptionsState
    {
        private readonly World world;
        private List<Nation> Nations => world.Nations;
        
        public NationMenu(ConsoleOptions sm) : base(sm)
        {
            Name = Options.Nation;
            AddOption("--create $name$", param => CreateNation(param.StringParam));
            AddOption("--list", param => PrintNations());
            AddOption("--manage $name$", param => ManageNation(param.StringParam));
            AddOption("--back", param => ChangeState(Options.Start));
        }

        public NationMenu(ConsoleOptions sm, World world) : this(sm)
        {
            this.world = world;
        }
        
        private void CreateNation(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Cannot create nation without name");
                return;
            }
            var nation = NationFactory.Instance.Create(name, world);
            if (nation == null)
            {
                Console.WriteLine($"Nation with name {name} is already exist. Try another");
                return;
            }
            PrintNations();
        }
        
        private void ManageNation(string name)
        {
            var managedNation = Nations.FirstOrDefault(nation => nation.Name == name);
            if (managedNation == null)
            {
                Console.WriteLine($"No nation with name {name}");
                return;
            }
            var state = sm.GetState(Options.SingleNation);
            ((SingleNationMenu) state).Nation = managedNation;
            ChangeState(Options.SingleNation);
        }
    
        private void PrintNations()
        {
            var line = Nations.Aggregate("", (current, nation) => current + $"{nation.Name}\n");
            Console.WriteLine(line);
        }
    }
}