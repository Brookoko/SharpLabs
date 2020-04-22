namespace Civilization.Resources
{
    public interface IResourceType
    {
        string Name { get; }
    }

    public abstract class ResourceType<T> : IResourceType where T : ResourceType<T>, new()
    {
        private static readonly ResourceType<T> Instance = new T();

        public T SingleInstance => (T) Instance;

        public abstract string Name { get; }
    }
}