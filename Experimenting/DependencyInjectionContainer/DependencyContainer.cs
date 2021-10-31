using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInjectionContainer
{
    public class DependencyContainer
    {
        private readonly List<Dependency> _dependencies = new();

        public void AddTransient<T>()
        {
            _dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Transient));
        }

        public void AddSingleton<T>()
        {
            _dependencies.Add(new Dependency(typeof(T), DependencyLifetime.Singleton));
        }

        public Dependency Get(Type type)
        {
            return _dependencies.First(x => x.Type.Name == type.Name);
        }

        public Dependency Get<T>()
        {
            return Get(typeof(T));
        }
    }
}