using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayService
{
    public static class ArrayHelper
    {
        public static int[] CreateArray()
        {
            var r = new Random();
            var array = new int[r.Next(1,20)];
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = r.Next(0, 101);
            }
            return array;
        }

       public static void ShowArray(int[] array)
        {
            Console.WriteLine();
            foreach (var i in array)
            {
                Console.Write($"{i}\t");
            }
            Console.WriteLine();
        }

       public static void Swap(ref int a, ref int b)
        {
            var t = a;
            a = b;
            b = t;
        }
    }
}
