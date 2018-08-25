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
            var number = -1L;
            while (number < 10000000000)
            {
                number++;
                if (SumOfDigits(number) != 10) continue;
                ClearCurrentConsoleLine();
                Console.Write(number);
                //var arr = InsertNumberIntoArray(number);
                if (!IsSuccess(number)) continue;
                Console.WriteLine($"The riddle solution is{Environment.NewLine}{number.ToString().PadLeft(10, '0')}");
                break;
            }

            Console.ReadKey();
        }

        static bool IsSuccess(long n)
        {
            var str = n.ToString().PadLeft(10, '0');
            return !str.Where((t, i) => t - '0' != str.Count(x => x == i + '0')).Any();
        }

        static int[] InsertNumberIntoArray(long n)
        {
            var arr = new int[10];
            for (int i = arr.Length - 1; i >= 0; i--)
            {
                arr[i] = (int)(n % 10);
                n /= 10;
                if (n == 0) break;
            }

            return arr;
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
