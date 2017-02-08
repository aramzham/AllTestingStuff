using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormattingNumericalData
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Values 999,99 & 99999 in various formats:");
            Console.WriteLine("c format: {0:c}", 999.99);
            Console.WriteLine("d format: {0:d10}", 99999); //cela pourrait etre {0:d9} par example
            Console.WriteLine("e format: {0:e}", 999.99);
            Console.WriteLine("E format: {0:E}", 999.99);
            Console.WriteLine("f format: {0:f1}", 999.99); //999.99 is rounded to 1000.0 in f1 formatting
            Console.WriteLine("g format: {0:g}", 999.99);
            Console.WriteLine("n format: {0:n}", 999.99);
            Console.WriteLine("x format: {0:x}", 99999);
            Console.WriteLine("X format: {0:X}", 99999);

            Console.ReadKey();
        }
    }
}
