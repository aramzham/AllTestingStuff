namespace DependencyInjectionContainer
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new DependencyContainer();
            container.AddTransient<HelloService>(); // when you change to singleton all numbers will be the same
            container.AddTransient<ServiceConsumer>();
            container.AddSingleton<MessageService>();

            var resolver = new DependencyResolver(container);

            var service1 = resolver.GetService<HelloService>();
            service1.Print();

            var service2 = resolver.GetService<ServiceConsumer>();
            service2.Print();

            var service3 = resolver.GetService<ServiceConsumer>();
            service3.Print();
        }
    }
}
