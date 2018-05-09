﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Test1
{
    class Program
    {
        //const string url = "https://e1-api.aws.kambicdn.com/offering/api/v2/ub/event/live/open.json?lang=en_GB&market=ZZ&client_id=2&channel_id=1&ncid={ConvertToUnixTimestamp(DateTime.UtcNow)}";
        private const string url = "https://www.marathonbet.com/en/live/popular.htm";
        static readonly string connectionString = "Data Source=oddsct01-lx.betconstruct.int;initial catalog=EntityLibrary.OddsContext;integrated security=False;User ID=sa;Password=JZKCcpd84FyIOr7a;TrustServerCertificate=True;MultipleActiveResultSets=True;App=EntityFramework";
        static void Main(string[] args)
        {
            //var phantomJs = new PhantomJS();
            //phantomJs.OutputReceived += (sender, e) => {
            //    Console.WriteLine($"PhantomJS output: {e.Data}");
            //};
            //phantomJs.RunScript("for (var i=0; i<10; i++) console.log('hello from js '+i); phantom.exit();", null);

            Console.ReadKey();
        }
        static bool almostIncreasingSequence(int[] sequence)
        {
            for (int i = 0; i < sequence.Length; i++)
            {
                var list = sequence.ToList();
                list.RemoveAt(i);
                if (list.OrderBy(x => x).SequenceEqual(list)) return true;
            }
            return false;
        }

        static List<MyClass> GetProxies()
        {
            const string query = "select * from myproxies";
            var employees = new List<MyClass>();
            using (var connection = new SqlConnection(connectionString))
            {
                var command = new SqlCommand(query, connection);
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    employees.Add(new MyClass { ip = (string)reader["IpAddress"], port = (int)reader["Port"], id = (int)reader["Id"] });
                }
                reader.Close();
            }
            return employees;
        }
    }

    class MyClass
    {
        public int id { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
    }
}
