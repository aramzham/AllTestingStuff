using System;

namespace ApplicationErrorCode
{
    class Program
    {
        //  create a *.bat file in bin folder and type this text in it
        /*  @echo off
            
            rem A batch file for ApplicationErrorCode.exe
            rem which captures the app's return value.
            
            ApplicationErrorCode
            @if "%ERRORLEVEL%" == "0" goto success
            
            :fail
             echo This application failed!
             echo return value = %ERRORLEVEL%
             goto end
            :success
             echo This application has succeeded!
             echo return value = %ERRORLEVEL%
             goto end
            :end
            echo All Done.
        */
        static int Main(string[] args)
        {
            Console.WriteLine("***** My First C# App *****");
            Console.WriteLine("Hello World!");
            Console.WriteLine();
            Console.ReadKey();

            return -1;
            //run the program, then open cmd.exe and navigate to the folder containing exe and bat files. Then type the bat file name and press Enter. You would see cmd showing our application has failed.
        }
    }
}
