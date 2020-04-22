namespace Civilization.People
{
    using System;
    using System.Collections.Generic;
    using Nations;
    using Resources;

    public interface IPersonFactory
    {
        List<Person> CreateDefaultPopulation(Nation nation);
        
        bool TryCreatePerson<T>(Nation nation, out Person person) where T : Person;
    }
    
    public class PersonFactory : IPersonFactory
    {
        private static PersonFactory instance;
        public static PersonFactory Instance => instance ?? (instance = new PersonFactory());
        
        private readonly Dictionary<Type, Resource> costPerPerson = new Dictionary<Type, Resource>
        {
            {typeof(Noble), 50.Of<Gold>()},
            {typeof(Warrior), 30.Of<Gold>()},
            {typeof(Worker), 10.Of<Gold>()}
        };
        private readonly Dictionary<string, Type> mapType = new Dictionary<string, Type>
        {
            {"noble", typeof(Noble)},
            {"warrior", typeof(Warrior)},
            {"worker", typeof(Worker)}
        }; 
        private int personCount;
        private Random random = new Random();
        
        public List<Person> CreateDefaultPopulation(Nation nation)
        {
            var people = new List<Person>();
            for (var i = 0; i < 10; i++)
            {
                InitPerson(new Worker(nation));
            }
            for (var i = 0; i < 5; i++)
            {
                InitPerson(new Warrior(nation));
            }
            for (var i = 0; i < 3; i++)
            {
                InitPerson(new Noble(nation));
            }
            return people;

            void InitPerson(Person person)
            {
                people.Add(person);
                person.Id = personCount + "";
                personCount++;
                person.Productivity = (float) random.NextDouble();
            }
        }

        public bool TryCreatePerson<T>(Nation nation, out Person person) where T : Person
        {
            var type = typeof(T);
            var cost = costPerPerson[type];
            if (nation.Wallet.CanSpent(cost))
            {
                person = CreateFromType(nation, type);
                return person != null;
            }
            person = null;
            return false;
        }
        
        public bool TryCreatePerson(Nation nation, string name, out Person person)
        {
            if (mapType.TryGetValue(name, out var type))
            {
                person = CreateFromType(nation, type);
                return person != null;
            }
            person = null;
            return false;
        }

        private Person CreateFromType(Nation nation, Type type)
        {
            var cost = costPerPerson[type];
            if (nation.Wallet.CanSpent(cost))
            {
                nation.Wallet.Spend(cost);
                var constructor = type.GetConstructor(new[] {typeof(Nation)});
                var person = (Person) constructor.Invoke(new object[] {nation});
                person.Id = personCount + "";
                personCount++;
                person.Productivity = (float) random.NextDouble();
                return person;
            }
            return null;
        }
    }
}