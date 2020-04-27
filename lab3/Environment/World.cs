namespace Environment
{
    using System.Collections.Generic;
    using System.Linq;

    public class World
    {
        public IEnumerable<Position> Positions => positions;
        
        private List<Position> positions = new List<Position>();

        public void AddPosition(Position position)
        {
            if (positions.All(p => p.x != position.x || p.y != position.y))
            {
                positions.Add(position);
            }
        }
    }
}