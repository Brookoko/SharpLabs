namespace Civilization.Worlds
{
    using System.Collections.Generic;
    using Nations;
    using Territories;

    public class World
    {
        public List<Nation> Nations { get; } = new List<Nation>();
        
        public List<Territory> Territories { get; } = new List<Territory>();
    }
}