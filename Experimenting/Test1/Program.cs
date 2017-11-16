using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new[] { 1, 3, 2, 1 };
            Console.WriteLine(almostIncreasingSequence(array));

            Console.ReadKey();
        }
        static bool almostIncreasingSequence(int[] sequence)
        {
            for (int i = 0; i < sequence.Length; i++)
            {
                var list = sequence.ToList();
                list.RemoveAt(i);
                if (list.OrderBy(x => x).SequenceEqual(list)) return true;
            }
            return false;
        }
    }
}
