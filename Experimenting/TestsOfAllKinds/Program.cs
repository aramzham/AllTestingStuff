using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using System.Threading;
using BetConstruct.OddsMarket.Live.Parsers.BL.Parsers.Bwin;
using Newtonsoft.Json;
using TestsOfAllKinds.Fonbet;
using TestsOfAllKinds._10Bet;
using System.Configuration;

namespace TestsOfAllKinds
{
    // Math.Floor() function:   The two numbers 123.456 and 123.987 are rounded down to the nearest integer.This means that regardless of how close they are close to 124, they are rounded to 123.
    //Note:
    //Floor can be useful when rounding numbers that are part of a larger representation of another number.

    //Discussion.The Math.Floor method when given a positive number will erase the digits after the decimal place. But when it receives a negative number, it will erase the digits and increase the number's negativity by 1.
    //So:
    //Using Math.Floor on a negative number will still decrease the total number.This means it will always become smaller.
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var connectionString = ConfigurationManager.ConnectionStrings["SuccinctlyDB"]?.ConnectionString;

                //using (var connection = new SqlConnection(@"Server=Computer\SqlExpress; Database=SuccinctlyExamples; Integrated Security=SSPI"))
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                }

                Console.WriteLine("Successfully opened and closed the database");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.WriteLine("Press any key to close");
            Console.ReadKey();
        }
    }
}
