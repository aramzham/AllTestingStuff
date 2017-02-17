using System;
using System.Linq;

namespace GetNumberWithoutConvertOrParse
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "LINQ + StackOverFlow :-)";
            Console.WriteLine(Convert("123456"));

            var a = new int[] { 6, 10, 25, 13, 20, 21, 11, 10, 18, 21 };
            var b = new int[] { 21, 10, 6, 0, 29, 25, 1, 17, 19, 25 };
            var v = 37;
            Console.WriteLine(sumOfTwo(a, b, v));

            Console.ReadKey();
        }

        static int Convert(string number)
        {
            return number.Select(x => x - '0').Select((t, i) => t * (int)Math.Pow(10, number.Length - i - 1)).Sum();
        }
        static bool sumOfTwo(int[] a, int[] b, int v)
        {
            //var line = a.Select(x => v - x);
            //return line.Any(b.Contains);
            if (a.Length < 1 || b.Length < 1) return false;
            var aList = a.ToList();
            var bList = b.ToList();
            aList.Sort();
            bList.Sort();
            //return bList.Any(t => aList.BinarySearch(v - t) >= -1);
            for (int i = 0; i < a.Length; i++)
            {
                if (v - a[i] > a.Max() || v - a[i] < a.Min()) return false;
                if (bList.BinarySearch(v - aList[i]) >= -1) return true;
            }
            return false;
        }
        static int strstr(string s, string x)
        {
            if (!s.Contains(x)) return -1;
            return s.IndexOf(x, StringComparison.Ordinal);
        }
    }
}
