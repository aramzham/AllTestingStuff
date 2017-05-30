using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindMax
{
    class Program
    {
        //TODO:
        static void Main(string[] args)
        {
            int[] array = { 1, 2, 3, 50, 4, 120 };
            Console.WriteLine(MaxFinder(array));

            Console.ReadKey();
        }

        private static int MaxFinder(int[] array)
        {
            if (array.Length == 1) return array[0];
            //return MaxFromTwoInts(MaxFinder(array.Take(array.Length / 2).ToArray()), MaxFinder(array.Skip(array.Length / 2).Take(array.Length / 2).ToArray()));
            var b = new int[array.Length/2];
            var c = new int[array.Length - b.Length];

            var i = 0;
            for (; i < b.Length; i++)
            {
                b[i] = array[i];
            }
            for (int k = 0; k < c.Length; k++)
            {
                c[k] = array[i++];
            }
            return Math.Max(MaxFinder(b), MaxFinder(c));
        }
    }
}
