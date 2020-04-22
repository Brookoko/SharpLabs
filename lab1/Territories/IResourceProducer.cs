namespace Civilization.Territories
{
    using System;
    using Resources;

    public interface IResourceProducer
    {
        float ProductivityPerSecond { get; }
        
        Resource CollectResourceFor(TimeSpan span);
    }
}