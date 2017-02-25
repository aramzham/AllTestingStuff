using System;
using System.Collections.Generic;
using System.Linq;

namespace Larry_Array
{
    class Program
    {
        static void Main(string[] args) //couldn't really understand the purpose of the task
        {
            var numberOfTestCases = int.Parse(Console.ReadLine());
            var lenghtOfArray = 0;
            var array = new int[] { };
            for (int i = 0; i < numberOfTestCases; i++)
            {
                //lenghtOfArray = int.Parse(Console.ReadLine());
                //array = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                //array = new int[] { 9, 6, 8, 12, 3, 7, 1, 11, 10, 2, 5, 4 };//no
                array = new int[] { 17, 21, 2, 1, 16, 9, 12, 11, 6, 18, 20, 7, 14, 8, 19, 10, 3, 4, 13, 5, 15 };
                Console.WriteLine(CanBeSorted(array) ? "YES" : "NO");
            }
        }
        //5
        //12
        //9 6 8 12 3 7 1 11 10 2 5 4//NO
        //21
        //17 21 2 1 16 9 12 11 6 18 20 7 14 8 19 10 3 4 13 5 15//YES
        //15
        //5 8 13 3 10 4 12 1 2 7 14 6 15 11 9//NO
        //13
        //8 10 6 11 7 1 9 12 3 5 13 4 2//YES
        //18
        //7 9 15 8 10 16 6 14 5 13 17 12 3 11 4 1 18 2//NO
        private static bool CanBeSorted(int[] array)
        {
            var rotated = new int[3];
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] < array[i + 1]) continue;
                for (int j = 0; j < 2; j++) //turn 2 times
                {
                    if (i == array.Length - 2)
                    {
                        i--;
                    }
                    rotated = RotateLeft(array.ToList().GetRange(i, 3)).ToArray(); // rotate selected 3 elements
                    for (int k = i; k < i + 3; k++)
                    {
                        array[k] = rotated[k - i];
                    }
                    //i++;
                    if (array.SequenceEqual(array.OrderBy(x => x))) return true; // if sequence is ordered than return true
                }
            }
            return false;
        }

        private static IEnumerable<T> RotateLeft<T>(IEnumerable<T> container)
        {
            return container.Skip(1).Concat(container.Take(1));
        }
    }
}
