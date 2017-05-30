using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HanoiTower
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Enter a number");
                var n = int.Parse(Console.ReadLine());
                var A = new List<int>();
                for (int i = 0; i < n; i++)
                {
                    A.Add(n - i);
                    Console.Write($"{A[i]}\t");
                }

                var B = new List<int>();
                var C = new List<int>();
                Tower(n, A, B, C);
                ShowList(C);
            }
        }

        static void Tower(int n, List<int> source, List<int> bridge, List<int> destination)
        {
            if (n == 1)
            {
                destination.Add(source[source.Count - 1]);
                source.RemoveAt(source.Count - 1);
            }
            else
            {
                Tower(n - 1, source, destination, bridge);
                Tower(1, source, bridge, destination);
                Tower(n - 1, bridge, source, destination);
            }
        }

        private static void ShowList(IEnumerable<int> COLLECTION)
        {
            foreach (var i in COLLECTION)
            {
                Console.Write($"{i}\t");
            }
            Console.WriteLine();
        }
    }
}
