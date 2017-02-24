using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Circular_Array_Rotation
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int k = Convert.ToInt32(tokens_n[1]);
            int q = Convert.ToInt32(tokens_n[2]);
            string[] a_temp = Console.ReadLine().Split(' ');
            int[] a = Array.ConvertAll(a_temp, Int32.Parse);
            var indexes = new int[q];
            for (int a0 = 0; a0 < q; a0++)
            {
                int m = Convert.ToInt32(Console.ReadLine());
                indexes[a0] = m;
            }
            var list = a.ToList();
            var last = 0;
            for (int i = 0; i < k; i++)
            {
                last = list.Last();
                list.RemoveAt(list.Count-1);
                list.Insert(0,last);
            }
            foreach (var i in indexes)
            {
                Console.WriteLine(list[i]);
            }
        }
    }
}
