namespace Environment
{
    using Entities;

    public struct Position
    {
        public int x;
        
        public int y;
        
        public PositionType type;
        
        public Position(int x, int y, PositionType type)
        {
            this.x = x;
            this.y = y;
            this.type = type;
        }
        
        public bool CanTraverse(IMovable movable)
        {
            if ((movable.MovementType & MovementType.Walk) != 0 && type == PositionType.Ground) return true;
            if ((movable.MovementType & MovementType.Fly) != 0 && type == PositionType.Air) return true;
            if ((movable.MovementType & MovementType.Swim) != 0 && type == PositionType.Water) return true;
            return false;
        }
    }

    public enum PositionType
    {
        Ground,
        Air,
        Water,
    }
}