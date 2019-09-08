using System;
using System.Collections.Generic;
using System.Linq;

namespace TestsOfAllKinds
{
    // Math.Floor() function:   The two numbers 123.456 and 123.987 are rounded down to the nearest integer.This means that regardless of how close they are close to 124, they are rounded to 123.
    //Note:
    //Floor can be useful when rounding numbers that are part of a larger representation of another number.

    //Discussion.The Math.Floor method when given a positive number will erase the digits after the decimal place. But when it receives a negative number, it will erase the digits and increase the number's negativity by 1.
    //So:
    //Using Math.Floor on a negative number will still decrease the total number.This means it will always become smaller.
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadLine();
        }
        
        private static bool IsSquare(int n)
        {
            int i = 1;
            while (true)
            {
                if (n < 0)
                    return false;
                if (n == 0)
                    return true;
                n -= i;
                i += 2;
            }
        }
    }
}
