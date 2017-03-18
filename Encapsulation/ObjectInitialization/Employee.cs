using System;

namespace ObjectInitialization
{
    partial class Employee
    {
        public void GetBonus(int sum)
        {
            Salary += sum;
        }

        public void Display()
        {
            Console.WriteLine($"{Name} gets {Salary}$ salary");
        }
    }
}
