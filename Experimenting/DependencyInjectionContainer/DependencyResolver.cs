using System;
using System.Linq;

namespace DependencyInjectionContainer
{
    public class DependencyResolver
    {
        private readonly DependencyContainer _container;

        public DependencyResolver(DependencyContainer container)
        {
            _container = container;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public object GetService(Type type)
        {
            var dependency = _container.Get(type);
            var constructor = dependency.Type.GetConstructors().Single();
            var parameters = constructor.GetParameters().ToArray();

            if (parameters.Length > 0)
            {
                var parameterImplementations = new object[parameters.Length];

                for (int i = 0; i < parameters.Length; i++)
                {
                    parameterImplementations[i] = GetService(parameters[i].ParameterType);
                }

                return CreateImplementation(dependency, t => Activator.CreateInstance(t, parameterImplementations));
            }

            return CreateImplementation(dependency, Activator.CreateInstance);
        }

        public object CreateImplementation(Dependency dependency, Func<Type, object> factory)
        {
            if (dependency.IsImplemented)
            {
                return dependency.Implementation;
            }

            var implementation = factory(dependency.Type);

            if (dependency.Lifetime == DependencyLifetime.Singleton)
            {
                dependency.AddImplementation(implementation);
            }

            return implementation;
        }
    }
}