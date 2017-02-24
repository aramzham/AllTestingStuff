using System;
using System.Collections.Generic;
using System.Linq;

namespace Larry_Array
{
    class Program
    {
        static void Main(string[] args)
        {
            var numberOfTestCases = int.Parse(Console.ReadLine());
            var lenghtOfArray = 0;
            var array = new int[] { };
            for (int i = 0; i < numberOfTestCases; i++)
            {
                //lenghtOfArray = int.Parse(Console.ReadLine());
                //array = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                array = new int[] { 1, 2, 3, 5, 4 };
                Console.WriteLine(CanBeSorted(array) ? "Yes" : "No");
            }
        }

        private static bool CanBeSorted(int[] array)
        {
            var rotated = new int[3];
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] < array[i + 1]) continue;
                for (int j = 0; j < 2; j++)
                {
                    if (array.SequenceEqual(array.OrderBy(x => x))) return true;
                    rotated = RotateLeft(array.ToList().GetRange(i, 3)).ToArray();
                    Array.Copy(array, i, rotated, 0, 3);
                }
            }
            return new bool();
        }

        private static IEnumerable<T> RotateLeft<T>(IEnumerable<T> container)
        {
            return container.Skip(1).Concat(container.Take(1));
        }
    }
}
