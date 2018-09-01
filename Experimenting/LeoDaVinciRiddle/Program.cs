using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LeoDaVinciRiddle
{
    class Program
    {
        static void Main(string[] args)
        {
            do
            {
                var number = -1L;
                var input = string.Empty;
                var length = 0;
                var success = false;
                do
                {
                    Console.Write("Enter a number:");
                    input = Console.ReadLine();
                } while (!int.TryParse(input, out length) || length <= 0);

                while (number < Math.Pow(10, length))
                {
                    number++;
                    if (SumOfDigits(number) != length) continue;
                    Console.Write(number);
                    ClearCurrentConsoleLine();
                    if (!IsSuccess(number, length)) continue;
                    success = true;
                    break;
                }

                Console.WriteLine(success
                    ? $"The riddle solution for length = {length} is {number.ToString().PadLeft(length, '0')}"
                    : $"There is no solution for length = {length}"); 
            } while (Console.ReadKey().Key != ConsoleKey.X);

            Console.ReadKey();
        }

        static bool IsSuccess(long n, int length)
        {
            var str = n.ToString().PadLeft(length, '0');
            return !str.Where((t, i) => t - '0' != str.Count(x => x == i + '0')).Any();
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        static int SumOfDigits(long n)
        {
            var sum = 0L;
            while (n != 0)
            {
                sum += n % 10;
                n /= 10;
            }
            return (int)sum;
        }
    }
}
