using System;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager = new Manager("John Terry", 35, 3456, 10000.21f, "an ssn", 2000); // we couldn't assign the ssn number
            manager.Display();

            Console.ReadKey();
        }
    }
}
