using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TestReceive
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(10000);
            Console.WriteLine("I'm on!");
            Receive("2");

            Console.ReadKey();
        }
        public static void Receive(string queue)
        {
            using (IConnection connection = new ConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue, true, false, false, null);
                    var consumer = new EventingBasicConsumer(channel);
                    BasicGetResult result = channel.BasicGet(queue, true);
                    if (result != null)
                    {
                        string data =
                            Encoding.UTF8.GetString(result.Body);
                        Console.WriteLine(data);
                    }
                }
            }
        }
    }
}
