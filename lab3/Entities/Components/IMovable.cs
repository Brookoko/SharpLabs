namespace Entities
{
    using System;
    using Environment;

    public interface IMovable
    {
        MovementType MovementType { get; }
        
        void Move(Position position);
    }

    [Flags]
    public enum MovementType
    {
        Walk = 1,
        Fly = 2,
        Swim = 4
    }
}