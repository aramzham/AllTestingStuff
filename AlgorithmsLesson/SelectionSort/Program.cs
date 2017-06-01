using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SelectionSort
{
    class Program
    {
        //TODO: hashvel sra bardutyun@
        //TODO: nayel bubble algorithm@, grel code-@
        static void Main(string[] args)
        {
            foreach (var item in new int[] { 3, 1, 20, 5 })
            {
                Console.Write($"{item}\t");
            }

            Console.ReadKey();
        }

        private static int Max(int[] a, int j)
        //private static int Max(int[] a)
        {
            var mx = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] > a[mx]) mx = i;
            }
            return mx;
            //return Array.IndexOf(a, a.Max());
        }

        private static void Swap(ref int a, ref int b)
        {
            var t = a;
            a = b;
            b = t;
        }

        private static int[] SelSort(int[] a)
        {
            var maxElement = 0;
            for (int i = 0; i < a.Length; i++)
            {
                //maxElement = Max(a, i);
                maxElement = Max(a, i);
                Swap(ref a[maxElement], ref a[a.Length - i - 1]);
            }
            return a;
        }
    }
}
