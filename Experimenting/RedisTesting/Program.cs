using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace RedisTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            var redisConfiguration = new RedisConfiguration()
            {
                AbortOnConnectFail = false,
                KeyPrefix = "_my_key_prefix_",
                Hosts = new RedisHost[]
                {
                    new RedisHost(){Host = "192.168.0.10", Port = 6379},
                    new RedisHost(){Host = "192.168.0.11",  Port =6379},
                    new RedisHost(){Host = "192.168.0.12",  Port =6379},
                    new RedisHost(){Host = "127.0.0.1", Port = 6379},
                },
                AllowAdmin = true,
                ConnectTimeout = 3000,
                Database = 0,
                Ssl = true,
                Password = "my_super_secret_password",
                ServerEnumerationStrategy = new ServerEnumerationStrategy()
                {
                    Mode = ServerEnumerationStrategy.ModeOptions.All,
                    TargetRole = ServerEnumerationStrategy.TargetRoleOptions.Any,
                    UnreachableServerAction = ServerEnumerationStrategy.UnreachableServerActionOptions.Throw
                }
            };
            var serializer = new NewtonsoftSerializer(new JsonSerializerSettings() { Formatting = Formatting.None });

            using (var cacheClient = new StackExchangeRedisCacheClient(serializer, "localhost:6379"))
            {
                var info = cacheClient.GetInfo();
                var added = cacheClient.Add("key", "Some Info", DateTimeOffset.Now.AddMinutes(1));
                var added1 = cacheClient.Add("notaKlyuch", "Not important Info", DateTimeOffset.Now.AddMinutes(10));
                foreach (var pair in cacheClient.GetAll<string>(new []{ "key" }))
                {
                    Console.WriteLine($"key: {pair.Key}, value: {pair.Value}");
                }
            }

            Console.ReadKey(); 
        }
    }
}
