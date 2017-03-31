using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestsOfAllKinds
{
    class Program
    {
        static void Main(string[] args)
        {
            var array = new[] {1, 34, 65, 333, 1123, 8908, 11200};
            var x = 11;
            Console.WriteLine(BinarySearch(array,x));

            Console.ReadKey();
        }

        static bool BinarySearch(int[] mynumbers, int target)
        {
            var found = false;
            var first =  0;
            var last = mynumbers.Length-1;
            var mid = (first + last)/2;

            //for a sorted array with descending values
            while (!found && first <= last)
            {
                mid = (first + last) / 2;

                if (target < mynumbers[mid])
                {
                    first = mid + 1;
                }

                if (target > mynumbers[mid])
                {
                    last = mid - 1;
                }

                else
                {
                    // You need to stop here once found or it's an infinite loop once it finds it.
                    found = true;
                }
            }
            return found;
        }
    }
}
