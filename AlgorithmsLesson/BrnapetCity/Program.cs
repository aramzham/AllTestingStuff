using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrnapetCity
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter numbers");
                var n = int.Parse(Console.ReadLine());
                var k = int.Parse(Console.ReadLine());
                Console.WriteLine(K_From_N(n, k));
            }
        }

        static int K_From_N(int n, int k)
        {
            if (k > n) return 0;
            if (k == n || k == 0) return 1;
            return K_From_N(n - 1, k - 1) + K_From_N(n - 1, k);
        }
    }
}
