using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UseOfVarKeyword
{
    class Program
    {
        //NOT PERMISSIBLE!!!!!!!!!!
        //private var myInt = 10;
        //NEITHER LIKE THIS!!!!!!!!
        //static var MyMethod(var x, var y)
        //{
        //}

        static void Main(string[] args)
        {
            var myInt = 0;
            var myBool = false;
            var myString = "Hello";
            Console.WriteLine($"{nameof(myInt)} is {myInt.GetType()}");
            Console.WriteLine($"{nameof(myBool)} is {myBool.GetType()}");
            Console.WriteLine($"{nameof(myString)} is {myString.GetType()}");

            //Cannot do anything like this
            //var myData;
            //Or
            //var myAnotherInt;
            //myAnotherInt = 10; //variable must be assigned at exact time of declaration
            //You can't assign a null
            //var myObj = null;

            //var is recommended while using LINQ queries
            var myArray = Enumerable.Range(0,100);
            var query = from i in myArray where i%2 == 0 select i;

            foreach (var num in query)
            {
                Console.Write($"{num}\t");
            }
            Console.WriteLine("\a"); // a beep :-)


            Console.ReadKey();
        }
    }
}
