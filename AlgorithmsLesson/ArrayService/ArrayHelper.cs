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
            var array = new int[r.Next(1, 20)];
            for (var i = 0; i < array.Length; i++)
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

        public static int[] MergeSortedArrays(int[] first, int[] second)
        {
            if (first is null || second is null)
                throw new ArgumentNullException("None of the arguments must be null");

            if (first.Length == 0)
                return second;
            if (second.Length == 0)
                return first;

            var resultArray = new int[first.Length + second.Length];

            var longer = first.Length > second.Length ? first : second;
            var shorter = first.Length <= second.Length ? first : second;
            var j = 0;

            for (var i = 0; i < longer.Length; i++)
            {
                for (; j < shorter.Length && longer[i] >= shorter[j]; j++)
                {
                    resultArray[i + j] = shorter[j];
                }

                resultArray[i + j] = longer[i];
            }

            return resultArray;
        }
    }
}
