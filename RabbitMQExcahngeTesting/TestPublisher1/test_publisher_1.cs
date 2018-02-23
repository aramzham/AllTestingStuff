using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Web.Script.Serialization;

namespace TestPublisher1
{
    class test_publisher_1
    {
        static void Main(string[] args)
        {
            var queue = "3";
            var age = 21;
            while (age!=40)
            {
                var mc = new MyClass() { Age = age, Coefficient = 1.3m, Name = "Edik" };
                var json = new JavaScriptSerializer().Serialize(mc);

                Send(queue, json);
                Thread.Sleep(2000);
                //Receive(queue);
                age++;
            }

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
                    channel.QueueBind(queue, "topicStyle", "client.#");
                    BasicGetResult result = channel.BasicGet(queue, true);
                    if (result != null)
                    {
                        string data = Encoding.UTF8.GetString(result.Body);
                        Console.WriteLine(data);
                    }
                }
            }
        }
        public static void Send(string queue, string data)
        {
            using (IConnection connection = new ConnectionFactory().CreateConnection())
            {
                using (IModel channel = connection.CreateModel())
                {
                    channel.ExchangeDeclare("topicStyle", ExchangeType.Topic, true, false, null);
                    channel.QueueDeclare(queue, true, false, false, null);
                    channel.BasicPublish("topicStyle", "client.service.logger", null, Encoding.UTF8.GetBytes(data));
                    channel.BasicPublish("topicStyle", "client.moment",null, Encoding.UTF8.GetBytes("Finland"));
                }
            }
        }
    }

    class MyClass
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Coefficient { get; set; }
    }
}
