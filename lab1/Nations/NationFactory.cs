namespace Civilization.Nations
{
    using System.Linq;
    using People;
    using Worlds;

    public interface INationFactory
    {
        Nation Create(string name, World world);
    }
    
    public class NationFactory : INationFactory
    {
        private static NationFactory instance;
        public static NationFactory Instance => instance ?? (instance = new NationFactory());
        
        public Nation Create(string name, World world)
        {
            if (string.IsNullOrEmpty(name) || IsNationsExist(name, world)) return null;
            var nation = new Nation(name);
            nation.Population.AddRange(PersonFactory.Instance.CreateDefaultPopulation(nation));
            world.Nations.Add(nation);
            return nation;
        }

        private bool IsNationsExist(string name, World world)
        {
            var nation = world.Nations.FirstOrDefault(n => n.Name == name);
            return nation != null && nation.Name == name;
        }
    }
}