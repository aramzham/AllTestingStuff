using System;

namespace CommandLineArguments
{
    class Program
    {
        static int Main(string[] args)
        {
//C: \Users\HP > C:\pathToBin\bin\Debug\CommandLineArguments.exe argument1 /argument2 -argumentik3
//enter the path to the executable file and type argument names, you'll then have the following output:
//            Arg: argument1
//            Arg: /argument2
//            Arg: -argumentik3
            foreach (var arg in args)
            {
                Console.WriteLine($"Arg: {arg}");
            }
            //for (int i = 0; i < args.Length; i++)
            //{
            //    Console.WriteLine($"Arg: {args[i]}");
            //}
            Console.ReadLine();
            return -1;
        }
    }
}


