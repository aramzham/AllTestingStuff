using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactorialCount
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                Console.WriteLine("Enter a number:");
                var n = int.Parse(Console.ReadLine());
                Console.WriteLine(GetFactorial(n));
            } while (true);
        }

        private static int GetFactorial(int n)
        {
            if (n < 2) return 1;
            return n * GetFactorial(n - 1);
        }
    }
}
