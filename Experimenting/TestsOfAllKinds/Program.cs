﻿using System;
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
using Newtonsoft.Json;

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

            //var proxy = new WebProxy("104.139.104.61:55533");
            //var handler = new HttpClientHandler(){Proxy = proxy};
            //var client = new HttpClient(handler) {Timeout = TimeSpan.FromSeconds(10)};

            var parser = new WilliamHillSeleniumParser();
            //parser.Initialize();
            var sw = new Stopwatch();
            var rnd = new Random();
            while (true)
            {
                try
                {
                    sw.Restart();
                    var bookmaker = parser.Parse();
                    sw.Stop();
                    if (bookmaker is null) continue;
                    bookmaker.ParseDuration = (int)sw.ElapsedMilliseconds;
                    var bookmakerJson = JsonConvert.SerializeObject(bookmaker);
                    Thread.Sleep(rnd.Next(1000, 3001));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Thread.Sleep(60 * 1000);
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
