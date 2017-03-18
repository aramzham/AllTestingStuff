using System;

namespace Inheritance
{
    class Program
    {
        static void Main(string[] args)
        {
            //var manager = new Manager("John Terry", 35, 3456, 10000.21f, "an ssn", 2000); // we couldn't assign the ssn number
            var manager = new Manager("33-444-666",100.66f,"Andros Townsend",44,8888,20);
            manager.Display();

            Console.ReadKey();
        }
    }
}
