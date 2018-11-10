using System;
using System.Collections.Generic;
using System.Data;
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
            //var v = new[] { "8","+","9","*","4","*","2","-","20"};
            //Console.WriteLine(P5(v));

            //var proxy = new WebProxy("104.168.157.236:3128");
            //var handler = new HttpClientHandler(){Proxy = proxy};
            //var client = new HttpClient(handler) {Timeout = TimeSpan.FromSeconds(10)};
            //client.DefaultRequestHeaders.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/70.0.3538.77 Safari/537.36");
            //var token = client.GetStringAsync("https://www.10bet.com/methods/sportscontent.ashx/GetAllLiveContent?").GetAwaiter().GetResult();

            var parser = new Bet10Parser();
            //parser.Initialize();
            var sw = new Stopwatch();
            var rnd = new Random();
            var cycleNumber = 0;
            while (true)
            {
                try
                {
                    sw.Restart();
                    var bookmaker = parser.Parse();
                    sw.Stop();
                    if (bookmaker is null) continue;
                    bookmaker.ParseDuration = (int)sw.ElapsedMilliseconds;
                    foreach (var match in bookmaker.Matches)
                    {
                        File.AppendAllText(@"E:\test\marketCounts.txt", $"{match.SportName} - {match.MatchMembers[0].Name} vs {match.MatchMembers[1].Name} | {match.Markets.Count}{Environment.NewLine}");
                    }
                    var bookmakerJson = JsonConvert.SerializeObject(bookmaker);
                    Thread.Sleep(rnd.Next(1000, 3001));
                    Console.WriteLine($"cycle {cycleNumber++} is done");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //Thread.Sleep(60 * 1000);
                }
            }

            Console.ReadKey();
        }
        static bool BinarySearch(int[] mynumbers, int target)
        {
            var found = false;
            var first = 0;
            var last = mynumbers.Length - 1;
            var mid = (first + last) / 2;

            //for a sorted array with descending values
            while (!found && first <= last)
            {
                mid = (first + last) / 2;

                if (target < mynumbers[mid])
                {
                    first = mid + 1;
                }

                if (target > mynumbers[mid])
                {
                    last = mid - 1;
                }

                else
                {
                    // You need to stop here once found or it's an infinite loop once it finds it.
                    found = true;
                }
            }
            return found;
        }

        static int P5(string[] elements)
        {
            var expression = string.Join("", elements);
            return (int)new DataTable().Compute(expression, "");
        }
    }
}
