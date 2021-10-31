using System;

namespace DependencyInjectionContainer
{
    public class MessageService
    {
        private readonly int _random;

        public MessageService()
        {
            _random = new Random().Next();
        }

        public string Message()
        {
            return $"Yo #{_random}";
        }
    }
}