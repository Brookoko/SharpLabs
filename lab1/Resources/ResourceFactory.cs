namespace Civilization.Resources
{
    using System;
    using System.Collections.Generic;

    public interface IResourceFactory
    {
        void Register<T>() where T : ResourceType<T>, new();
        
        Resource Create<T>(in float amount) where T : IResourceType, new();

        Resource Create(string type, in float amount);
        
        IResourceType TypeOf(string typeName);
        
        IResourceType TypeOf<T>();
    }
    
    public class ResourceFactory : IResourceFactory
    {
        private static ResourceFactory instance;
        public static ResourceFactory Instance => instance ?? (instance = new ResourceFactory());
        
        private readonly Dictionary<Type, IResourceType> currencies = new Dictionary<Type, IResourceType>();
        private readonly Dictionary<string, IResourceType> currenciesByName = new Dictionary<string, IResourceType>();
        
        public void Register<T>() where T : ResourceType<T>, new()
        {
            var temp = new T();
            currencies.Add(typeof(T), temp.SingleInstance);
            currenciesByName.Add(temp.Name, temp.SingleInstance);
        }
        
        public Resource Create<T>(in float amount) where T : IResourceType, new()
        {
            var type = typeof(T);
            if (!currencies.TryGetValue(type, out var resource))
            {
                ThrowNotFound(type);
            }
            return new Resource(resource, amount);
        }

        public Resource Create(string type, in float amount)
        {
            return new Resource(TypeOf(type), amount);
        }
        
        public IResourceType TypeOf(string typeName)
        {
            if (!currenciesByName.TryGetValue(typeName, out var resource))
            {
                ThrowNotFound(typeName);
            }
            return resource;
        }

        public IResourceType TypeOf<T>()
        {
            var type = typeof(T);
            if (!currencies.TryGetValue(type, out var resource))
            {
                ThrowNotFound(type);
            }
            return resource;
        }

        private void ThrowNotFound(Type type)
        {
            ThrowNotFound(type.Name);
        }
        
        private void ThrowNotFound(string name)
        {
            throw new KeyNotFoundException($"No such resource found {name}. Register resource before using it");
        }
    }

    public static class ResourceExtension
    {
        public static Resource Of<T>(this float amount) where T : IResourceType, new()
        {
            return ResourceFactory.Instance.Create<T>(amount);
        }
        
        public static Resource Of<T>(this int amount) where T : IResourceType, new()
        {
            return ResourceFactory.Instance.Create<T>(amount);
        }
    }
}