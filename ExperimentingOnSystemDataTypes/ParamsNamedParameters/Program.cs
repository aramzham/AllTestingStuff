using System;
using System.Linq;

namespace ParamsNamedParameters
{
    class Program
    {
        static void Main(string[] args)
        {
            var av = GetGeometricalAverage(3, 3, 81);
            Console.WriteLine(av);

            DisplayFancyMessage(ConsoleColor.Blue, ConsoleColor.DarkRed, "Shust");

            DisplayMessageNamed();
            DisplayMessageNamed(message: "Gotcha", textColor: ConsoleColor.DarkRed);

            Console.ReadKey();
        }

        static double GetGeometricalAverage(params double[] array)
        {
            if (array.Length == 0) return 0; 
            var product = array.Aggregate(1d, (current, num) => current*num);
            var n = 1d/array.Length;
            return Math.Pow(product, n);
        }

        static void DisplayFancyMessage(ConsoleColor textColor, ConsoleColor backgroundColor, string message)
        {
            var oldTextColor = Console.ForegroundColor;
            var oldBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);

            Console.ForegroundColor = oldTextColor;
            Console.BackgroundColor = oldBackgroundColor;
        }

        static void DisplayMessageNamed(ConsoleColor textColor = ConsoleColor.Cyan,
            ConsoleColor backgroundColor = ConsoleColor.Gray, string message = "Hello World!")
        {
            var oldTextColor = Console.ForegroundColor;
            var oldBackgroundColor = Console.BackgroundColor;

            Console.ForegroundColor = textColor;
            Console.BackgroundColor = backgroundColor;
            Console.WriteLine(message);

            Console.ForegroundColor = oldTextColor;
            Console.BackgroundColor = oldBackgroundColor;
        }
    }
}
