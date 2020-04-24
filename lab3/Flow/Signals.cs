namespace Flow
{
    using DependencyInjection.Signals;
    using Entities;
    using Environment;

    public class EntityMoving : Signal<Entity, Position> { }
    
    public class EntityAttacking : Signal<Entity, Entity> { }
    
    public class EntityHealing : Signal<Entity, Entity> { }
}