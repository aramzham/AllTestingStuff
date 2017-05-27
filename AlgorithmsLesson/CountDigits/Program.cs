using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountDigits
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Digits(1000000000));

            Console.ReadKey();
        }

        private static int Digits(int n)
        {
            if (n < 10) return 1;

            return 1 + Digits(n/10);
        }
    }
}
