using System;
using System.Linq;

namespace Sock_Merchant
{
    class Program
    {
        static void Main(string[] args)
        {
            //int n = Convert.ToInt32(Console.ReadLine());
            //string[] c_temp = Console.ReadLine().Split(' ');
            //int[] c = Array.ConvertAll(c_temp, int.Parse);
            var c = new[] { 10, 20, 20, 10, 10, 30, 50, 10, 20 };

            var distinct = c.Distinct();
            var socks = c.Distinct().Select(x => c.Count(a => a == x)/2).Sum();
            Console.WriteLine(socks);
            Console.ReadKey();
        }
    }
}
