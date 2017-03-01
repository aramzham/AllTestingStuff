using System;
using static System.Console;

namespace Constructor
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person();
            Person p2 = new Person("Aram");
            Person p3 = new Person(27);
            p3.SetPersonName("Gvidon");
            WriteLine(p3);
            Person p4 = new Person("Narek", 26);
            Person p5 = new Person(); //default ctor is called, not the one with optional parameters
            Person p6 = new Person(anotherField: true, age: 55);

            Console.ReadKey();
        }
    }

    class Person
    {
        private string name;
        private int age;
        private bool anotherField;
        private static string yardName;
        #region Constructors
        public Person(string name, int age) //master contstructor
        {
            Console.WriteLine("Master Ctor");
            this.name = name;
            if (age < 0) age = 0;
            this.age = age;
        }

        public Person(int age) : this("", age) //when chaining ctors with "this" we put all the work on one ctor - master ctor which will be called firstly, after master will be called the ctor with one parameter
        {
            Console.WriteLine("Ctor with int");
        }

        public Person(string name) : this(name, 0)
        {
            Console.WriteLine("Ctor with string");
        }

        public Person()
        {
            Console.WriteLine("Default ctor");
        }

        public Person(string name = "Babo", int age = 40, bool anotherField = false)
        {
            Console.WriteLine("Ctor with optional parameters");
        }
#endregion
        static Person() // static ctor doesn't have an access modifier, doesn't have parameters (can't be overloaded), is called one time (whenever a call to the class is executed), is called before all the other constructors
        {
            Console.WriteLine("Static ctor is the first!");
            yardName = "Masiv";
        }
        public void SetPersonName(string value)
        {
            name = value;
        }
    }
}
