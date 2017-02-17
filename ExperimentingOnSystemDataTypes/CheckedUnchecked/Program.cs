using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckedUnchecked
{
    class Program
    {
        static void Main(string[] args)
        {
            byte b1 = 100;
            byte b2 = 250;
            var sum = (byte)Add(b1,b2);
            Console.WriteLine(sum); // result -> 94 as 350 -256 = 94 => overflow

            //var sum2 = checked((byte)Add(b1, b2)); // this will result in an arithmetic operation exception
            //Console.WriteLine(sum2);
            try
            {
                var sum2 = checked((byte) Add(b1, b2));
                Console.WriteLine(sum2);
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }

            try
            {
                checked
                {
                    var sum3 = (byte) Add(b1, b2); // overflow is causing an OverflowException, so you'll be warned
                    Console.WriteLine(sum3);
                }
            }
            catch (OverflowException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }

        static int Add(int x, int y)
        {
            return x + y;
        }
    }
}
