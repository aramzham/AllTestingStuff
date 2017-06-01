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
            //var rnd = new Random();
            //var size = rnd.Next(6, 24);
            //var a = new int[size];
            //Console.WriteLine($"Size = {size}");
            //for (int i = 0; i < a.Length; i++)
            //{
            //    a[i] = rnd.Next(-99, 100);
            //}
            //Array.Sort(a);
            //foreach (var x in a)
            //{
            //    Console.Write($"{x}\t");
            //}

            //do
            //{
            //    Console.WriteLine("Enter a number");
            //    var n = int.Parse(Console.ReadLine());
            //    //Console.WriteLine(FindBinary(a, 0, a.Length - 1, n));
            //    Console.WriteLine(FindBinary(a,20));
            //} while (true);

            var a = new int[] {7, 91, 125, 430, 530};
            var n = 12;
            Console.WriteLine(FindBinary(a,n));

            Console.ReadKey();
        }

        private static int FindBinary(int[] array, int begin, int end, int target)
        {
            //if (begin > end) return -1;
            //var middle = begin + (end - begin) / 2;
            //if (array[middle] == target) return middle;
            //if (array[middle] > target) return FindBinary(array, begin, middle - 1, target);
            //else return FindBinary(array, middle + 1, end, target);

            while (true)
            {
                if (begin > end) return -1;
                var middle = begin + (end - begin) / 2;

                if (array[middle] == target) return middle;
                if (array[middle] > target) end = middle - 1;
                else begin = middle + 1;
            }
        }

        private static int FindBinary(int[] array, int n)
        {
            var first = 0;
            var last = array.Length - 1;
            var middle = last/2;

            while (first<=last)
            {
                if (n == array[middle]) return middle;
                if (n > array[middle]) first = middle + 1;
                else last = middle - 1;

                middle = first + (last - first)/2;
            }
            return -1;
        }

        // int[] array
        // begin-------------------------middle--------------------------end
    }
}
