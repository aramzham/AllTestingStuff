using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace TestClient1
{
    class test_Client_1
    {
        static void Main(string[] args)
        {
            Thread.Sleep(10000);
            Receive("3");

            Console.ReadKey();
        }
        public static void Receive(string queue)
        {
            using (IConnection connection = new ConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("topicStyle", ExchangeType.Topic, true, false, null);
                    channel.QueueDeclare(queue, true, false, false, null);
                    channel.QueueBind(queue, "topicStyle", "*.service.*");
                    BasicGetResult result = channel.BasicGet(queue, true);
                    if (result != null)
                    {
                        string data = Encoding.UTF8.GetString(result.Body);
                        Console.WriteLine($"{data}[{DateTime.Now}]");
                    }
                }
            }
        }
    }
}
