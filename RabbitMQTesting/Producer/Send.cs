using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace Producer
{
    class Send
    {
        public static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("task_queue", durable : true, exclusive: false,  autoDelete:false, arguments:null);
                    var message = GetMessage(args);
                    var body = Encoding.UTF8.GetBytes(message); // creating byte[] from string

                    var properties = channel.CreateBasicProperties();
                    properties.Persistent = true;

                    channel.BasicPublish("", routingKey: "task_queue", basicProperties: properties, body: body);
                    Console.WriteLine($" [x]  Sent {message}");
                }
            }

            Console.WriteLine(" Press [enter] to exit");
            Console.ReadKey();
        }

        private static string GetMessage(string[] args)
        {
            return ((args.Length > 0) ? string.Join(" ", args) : "Hello aper!");
        }
    }
}
