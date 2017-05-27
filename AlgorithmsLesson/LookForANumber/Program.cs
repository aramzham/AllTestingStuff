using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LookForANumber
{
    class Program
    {
        static void Main(string[] args)
        {
            var rnd = new Random();
            var size = rnd.Next(6, 24);
            var a = new int[size];
            Console.WriteLine($"Size = {size}");
            for (int i = 0; i < a.Length; i++)
            {
                a[i] = rnd.Next(-99, 100);
            }
            Array.Sort(a);
            foreach (var x in a)
            {
                Console.Write($"{x}\t");
            }

            do
            {
                Console.WriteLine("Enter a number");
                var n = int.Parse(Console.ReadLine());
                Console.WriteLine(FindBinary(a, 0, a.Length - 1, n));
            } while (true);

            Console.ReadKey();
        }

        private static int FindBinary(int[] array, int begin, int end, int target)
        {
            if (begin > end) return -1;
            var middle = begin + (end - begin) / 2;
            if (array[middle] == target) return middle;
            if (array[middle] > target) return FindBinary(array, begin, middle - 1, target);
            else return FindBinary(array, middle + 1, end, target);
        }
        // int[] array
        // begin-------------------------middle--------------------------end
    }
}
