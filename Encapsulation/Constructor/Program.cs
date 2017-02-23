using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constructor
{
    class Program
    {
        static void Main(string[] args)
        {
            Person p1 = new Person();
            Person p2 = new Person("Aram");
            Person p3 = new Person(27);
            Person p4 = new Person("Narek",26);

        }
    }

    class Person
    {
        private string name;
        private int age;

        public Person(string name, int age) //master contstructor
        {
            this.name = name;
            if (age < 0) age = 0;
            this.age = age;
        }

        public Person(int age) : this("", age)
        {

        }

        public Person(string name) : this(name, 0)
        {

        }

        public Person()
        {
            
        }

        public void SetPersonName(string value)
        {
            name = value;
        }
    }
}
