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

            Console.ReadKey();
        }

        public static int Convert(string number)
        {
            return number.Select(x => x - '0').Select((t, i) => t * (int)Math.Pow(10, number.Length - i - 1)).Sum();
        }
    }
}
