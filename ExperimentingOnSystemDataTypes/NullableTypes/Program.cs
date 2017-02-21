using System;

namespace NullableTypes
{
    class Program
    {
        static void Main(string[] args)
        {
            int? x = null;
            if (x == null) x = 100;
            Console.WriteLine($"x = {x}");

            int? y = 30;
            Console.WriteLine($"y = {y ?? 100}"); //this means that if y==null we will give y a default value of 100

            int[] z = new int[3];
            Console.WriteLine($"z = {z?.Length ?? 20}"); //here we check if z is not null we will print its length, if z is null then we will print 20 by default, so we can use these two (? and ??) operators and avoid if/else statements

            Console.ReadKey();
        }
    }
}
