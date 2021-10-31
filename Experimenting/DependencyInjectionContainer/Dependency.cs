using System;

namespace DependencyInjectionContainer
{
    public class Dependency
    {
        public Dependency(Type t, DependencyLifetime l)
        {
            Type = t;
            Lifetime = l;
        }

        public Type Type { get; set; }
        public DependencyLifetime Lifetime { get; set; }
        public object Implementation { get; set; }
        public bool IsImplemented { get; set; }

        public void AddImplementation(object i)
        {
            Implementation = i;
            IsImplemented = true;
        }
    }
}