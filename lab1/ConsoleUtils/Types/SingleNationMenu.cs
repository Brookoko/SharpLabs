namespace Civilization
{
    using System;
    using System.Linq;
    using Nations;
    using People;
    using Resources;
    using Worlds;

    public class SingleNationMenu : ConsoleOptionsState
    {
        public Nation Nation { get; set; }
        private World world;
        
        public SingleNationMenu(ConsoleOptions options) : base(options)
        {
            Name = Options.SingleNation;
            AddOption("--collect", param => Collect());
            AddOption("--resources", param => PrintResources());
            AddOption("--acquire $id$", param => AcquireTerritory(param.StringParam));
            AddOption("--terr", param => PrintTerritories());
            AddOption("--people", param => PrintPopulation());
            AddOption("--createPerson $name$", param => CreatePerson(param.StringParam));
            AddOption("--move $person$ $terr$", param => MovePersonTo(param.StringParameters[0], param.StringParameters[1]));
            AddOption("--back", param => ChangeState(Options.Nation));
        }

        public SingleNationMenu(ConsoleOptions options, World world) : this(options)
        {
            this.world = world;
        }

        private void Collect()
        {
            var now = DateTime.UtcNow;
            foreach (var territory in Nation.Territories)
            {
                var resource = territory.CollectResourceFor(now - Nation.LastResourceCollection);
                Nation.Wallet.Add(resource);
            }
            Nation.LastResourceCollection = now;
            PrintResources();
        }

        private void PrintResources()
        {
            var gold = $"Gold ({Nation.Wallet.GetAmountOf<Gold>().Amount})\n";
            var wood = $"Wood ({Nation.Wallet.GetAmountOf<Wood>().Amount})\n";
            var stone = $"Stone ({Nation.Wallet.GetAmountOf<Stone>().Amount})\n";
            Console.WriteLine(gold + wood + stone);
        }

        private void AcquireTerritory(string id)
        {
            var ter = world.Territories.FirstOrDefault(t => t.Id == id);
            if (ter == null)
            {
                Console.WriteLine($"Cannot find territory with id {id}");
                return;
            }
            if (ter.TryAcquire(Nation))
            {
                Console.WriteLine($"Acquire territory {id}");
            }
            else
            {
                Console.WriteLine($"Cannot acquire territory {id}");
            }
        }

        private void PrintTerritories()
        {
            var line = Nation.Territories.Aggregate("", (current, ter) => current + $"{ter.Id} ({ter.GetType().Name})\n");
            Console.WriteLine(line);
        }

        private void CreatePerson(string name)
        {
            if (PersonFactory.Instance.TryCreatePerson(Nation, name, out var person))
            {
                Nation.Population.Add(person);
            }
            Console.WriteLine($"Cannot create person with name {name}");
        }

        private void PrintPopulation()
        {
            var line = Nation.Population.Aggregate("", (current, p) => current + $"{p.Id} ({p.GetType().Name})\n");
            var noble = Nation.Population.Count(p => p is Noble);
            var warrior = Nation.Population.Count(p => p is Warrior);
            var worker = Nation.Population.Count(p => p is Worker);
            Console.WriteLine($"Noble ({noble})\nWarrior({warrior})\nWorker({worker})\n");
            Console.WriteLine(line);
        }
        
        private void MovePersonTo(string personId, string terrId)
        {
            Console.WriteLine($"P: {personId}");
            Console.WriteLine($"T: {terrId}");
            var person = Nation.Population.FirstOrDefault(p => p.Id == personId);
            if (person == null)
            {
                Console.WriteLine($"No person with id: {personId}");
                return;
            }
            var terr = Nation.Territories.FirstOrDefault(t => t.Id == terrId);
            if (terr == null)
            {
                Console.WriteLine($"No acquired territory with id: {terrId}");
                return;
            }
            terr.AddPeople(new []{ person });
        }
    }
}