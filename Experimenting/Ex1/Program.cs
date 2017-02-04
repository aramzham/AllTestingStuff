using System;

namespace Ex1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("What is the chance that 2 people will have the same birth date in group?\n");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("Enter the number of people in your group: ");
            var n = int.Parse(Console.ReadLine());
            var prob = TwoSameBirthDates(n);
            Console.WriteLine($"There is a {prob:0.00}% chance that 2 people will have same birth date");


            Console.ReadKey();
        }

        public static double TwoSameBirthDates(int count)
        {
            double inverseProbability = 1;
            for (int i = 0; i < count; i++)
            {
                inverseProbability *= (365 - i) / 365d;
            }
            return (1d - inverseProbability) * 100;
        }
    }
}
