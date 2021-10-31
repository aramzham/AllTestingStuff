using System;

namespace DependencyInjectionContainer
{
    public class HelloService
    {
        private readonly MessageService _messageService;
        private readonly int _random;

        public HelloService(MessageService messageService)
        {
            _messageService = messageService;
            _random = new Random().Next();
        }

        public void Print()
        {
            Console.WriteLine($"Hello #{_random} world, {_messageService.Message()}");
        }
    }
}