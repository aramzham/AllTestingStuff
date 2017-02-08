using System;

namespace CommandLineArguments
{
    class Program
    {
//        static int Main(string[] args)
//        {
////C: \Users\HP > C:\pathToBin\bin\Debug\CommandLineArguments.exe argument1 /argument2 -argumentik3
////enter the path to the executable file and type argument names, you'll then have the following output:
////            Arg: argument1
////            Arg: /argument2
////            Arg: -argumentik3
//            foreach (var arg in args)
//            {
//                Console.WriteLine($"Arg: {arg}");
//            }
//            //for (int i = 0; i < args.Length; i++)
//            //{
//            //    Console.WriteLine($"Arg: {args[i]}");
//            //}
//            Console.ReadLine();
//            return -1;
//        }
        static void Main()
        {
            //I've added some arguments in Project->Properties->Debug->Start options->Command line arguments
            var args = Environment.GetCommandLineArgs();
            foreach (var arg in args)
            {
                Console.WriteLine($"Arg: {arg}");
            }
            ShowEnvironementDetails(); //this method uses System.Environment class and tells everything about the operating system
            Console.ReadKey();
        }

        private static void ShowEnvironementDetails()
        {
            foreach (var logicalDrive in Environment.GetLogicalDrives())
            {
                Console.WriteLine($"Drive: {logicalDrive}");
            }
            Console.WriteLine($"OS: {Environment.OSVersion}");
            Console.WriteLine($"Number of processors: {Environment.ProcessorCount}");
            Console.WriteLine($".Net version: {Environment.Version}");
            Console.WriteLine($"Exit code: {Environment.ExitCode}");
            Console.WriteLine($"Is OS 64-bit?: {Environment.Is64BitOperatingSystem}");
            Console.WriteLine($"Machine name: {Environment.MachineName}");
            Console.WriteLine($"New line symbol: {Environment.NewLine}");
            Console.WriteLine($"System dirctory: {Environment.SystemDirectory}");
            Console.WriteLine($"User name: {Environment.UserName}");
        }
    }
}


