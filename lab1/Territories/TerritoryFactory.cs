namespace Civilization.Territories
{
    using System;
    using System.Collections.Generic;

    public interface ITerritoryFactory
    {
        Territory Create(string typeName);

        IEnumerable<Territory> CreateRandom(int count);

        IEnumerable<string> Keys();
    }
    
    public class TerritoryFactory : ITerritoryFactory
    {
        private static TerritoryFactory instance;
        public static TerritoryFactory Instance => instance ?? (instance = new TerritoryFactory());
        
        private readonly Dictionary<string, Type> typeMap = new Dictionary<string, Type>
        {
            {"forest", typeof(Forest)},
            {"castle", typeof(Castle)},
            {"mine", typeof(Mine)}
        };
        private int territoryCount;
        private readonly Random random = new Random();
        
        public Territory Create(string typeName)
        {
            if (typeMap.TryGetValue(typeName, out var type))
            {
                var territory = (Territory) Activator.CreateInstance(type);
                territory.Id = territoryCount + "";
                territory.ProductivityPerSecond = (float) random.NextDouble() * 10f;
                territoryCount++;
                return territory;
            }
            return null;
        }
        
        public IEnumerable<Territory> CreateRandom(int count)
        {
            var territories = new List<Territory>();
            for (var i = 0; i < count; i++)
            {
                var keys = new string[typeMap.Count];
                typeMap.Keys.CopyTo(keys, 0);
                var index = random.Next(keys.Length);
                var key = keys[index];
                territories.Add(Create(key));
            }
            return territories;
        }

        public IEnumerable<string> Keys()
        {
            return typeMap.Keys;
        }
    }
}