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

        public static int Convert(string number)
        {
            return number.Select(x => x - '0').Select((t, i) => t * (int)Math.Pow(10, number.Length - i - 1)).Sum();
        }
        public static bool sumOfTwo(int[] a, int[] b, int v)
        {
            //var line = a.Select(x => v - x);
            //return line.Any(b.Contains);
            if (a.Length < 1 || b.Length < 1) return false;
            var aList = a.ToList();
            var bList = b.ToList();
            aList.Sort();
            bList.Sort();
            var index = 0;
            for (int i = 0; i < bList.Count; i++)
            {
                if (aList.BinarySearch(v - bList[i]) >= -1) return true;
            }
            return false;
        }
    }
}
