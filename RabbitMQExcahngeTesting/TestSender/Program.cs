using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TestSender
{
    class Program
    {
        static void Main(string[] args)
        {
            var counter = 0;
            while (counter < 10)
            {
                Send("2", counter.ToString());
                Receive("2");
                counter++;
            }

            Console.ReadKey();
        }
        public static void Send(string queue, string data)
        {
            using (IConnection connection = new ConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, true, false, false, null);
                    channel.BasicPublish(string.Empty, queue, null, Encoding.UTF8.GetBytes(data));
                }
            }
        }
        public static void Receive(string queue)
        {
            using (IConnection connection = new ConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, true, false, false, null);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume("Killer", false, consumer);
                }
            }
        }
    }
}
