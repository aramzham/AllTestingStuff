using System;

namespace Inheritance
{
    partial class Employee
    {
        public float Salary { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int EmployeeID { get; set; }
        public void GetBonus(int sum)
        {
            Salary += sum;
        }

        public void Display()
        {
            Console.WriteLine($"{Name} gets {Salary}$ salary, he is {Age} years old, his ID is {EmployeeID}, with social security number of {ssn}");
        }
    }
}
