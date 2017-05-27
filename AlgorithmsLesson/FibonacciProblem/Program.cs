using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FibonacciProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Fibonacci(49));

            Console.ReadKey();
        }

        private static int Fibonacci(uint n) //uint to not give negative values
        {
            if (n <= 2) return 1;
            return Fibonacci(n - 2) + Fibonacci(n - 1);
        }
    }
}
