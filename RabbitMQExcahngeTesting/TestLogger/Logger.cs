using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestLogger
{
    class Logger
    {
        static void Main(string[] args)
        {
            Thread.Sleep(15000);
            var queue = "log";
            IConnection connection = new ConnectionFactory().CreateConnection();


            IModel channel = connection.CreateModel();

            channel.ExchangeDeclare("topicStyle", ExchangeType.Topic, true, false, null);
            channel.QueueDeclare(queue, true, false, false, null);
            channel.QueueBind(queue, "topicStyle", "*.*.logger");
            while (channel.IsOpen)
            {
                BasicGetResult result = channel.BasicGet(queue, true);
                if (result != null)
                {
                    string data = Encoding.UTF8.GetString(result.Body);
                    //File.AppendAllText("D:\\Edik\\rabbitLog.log", $"{data} [{DateTime.Now}]{Environment.NewLine}");
                    Console.WriteLine(data);
                }
            }
            channel.Close();
            connection.Close();

            Console.WriteLine("No more messages left");
            Console.ReadKey();
        }
    }
}
