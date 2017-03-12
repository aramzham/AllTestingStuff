using System;
using System.Collections.Generic;
using System.Linq;

namespace Larry_Array
{
    class Program
    {
        static void Main(string[] args) //couldn't really understand the purpose of the task
        {
            int t = Convert.ToInt32(Console.ReadLine());
            for (int a0 = 0; a0 < t; a0++)
            {
                int n = Convert.ToInt32(Console.ReadLine());
                if(n<3) Console.WriteLine(-1);
                else
                {
                    
                }
            }

            Console.ReadKey();
        }
        //4  
        //1  -1
        //3  555
        //5  33333
        //11 55555533333
        //https://www.hackerrank.com/challenges/sherlock-and-the-beast
        static bool CanBeSorted(int[] array)
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
        static IEnumerable<T> RotateLeft<T>(IEnumerable<T> container)
        {
            return container.Skip(1).Concat(container.Take(1));
        }
    }
}
