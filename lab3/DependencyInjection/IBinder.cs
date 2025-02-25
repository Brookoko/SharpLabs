namespace DependencyInjection
{
    using System;
    using System.Collections.Generic;

    public interface IBinder
    {
        IBinder Bind<T>();
        
        IBinder Bind(Type type);
        
        IBinder To<T>() where T : class, new();
        
        IBinder To(Type type);
        
        void ToInstance(object obj);
        
        void ToSingleton();
    }

    public class Binder : IBinder
    {
        private Type binding;
        private Mapping mapping;
        private TypeMapping map;
        
        private readonly List<IBinder> multipleBindings = new List<IBinder>();
        
        public Binder(Type binding, TypeMapping map)
        {
            this.binding = binding;
            this.map = map;
            mapping = map.Create(binding);
        }

        public IBinder Bind<T>()
        {
            return Bind(typeof(T));
        }

        public IBinder Bind(Type type)
        {
            var binder = new Binder(type, map);
            multipleBindings.Add(binder);
            return this;
        }

        public IBinder To<T>() where T : class, new()
        {
            return To(typeof(T));
        }

        public IBinder To(Type type)
        {
            if (!binding.IsAssignableFrom(type))
                throw new BindingException($"Type: {type} doesn't implement {binding}");
            mapping.Type = type;
            foreach (var binder in multipleBindings)
            {
                binder.To(type);
            }
            return this;
        }
        
        public void ToInstance(object obj)
        {
            if (!binding.IsInstanceOfType(obj)) throw new BindingException($"Instance: {obj} doesn't implement {binding}");
            mapping.Instance = obj;
            foreach (var binder in multipleBindings)
            {
                binder.ToInstance(obj);
            }
        }

        public void ToSingleton()
        {
            var constructor = mapping.Type.GetConstructor(Type.EmptyTypes);
            var singleton = constructor?.Invoke(new object[] { });
            ToInstance(singleton);
        }
    }
}