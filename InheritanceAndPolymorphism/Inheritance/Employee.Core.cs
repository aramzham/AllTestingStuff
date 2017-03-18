using System;

namespace Inheritance
{
    partial class Employee //name the file -name-.-something-.cs to make it partial with -name-.cs file
    {
        public Employee() : this("an ssn", 10f,"unknown artist", 27, 0)
        {
            Console.WriteLine("Employee default ctor is used");
        }

        public Employee(string ssn, float salary, string name, int age, int employeeId) : this(salary,name,age, employeeId)
        {
            Console.WriteLine("Employee parametrized ctor is being used");
            this.ssn = ssn;
            //Salary = salary;
            //Name = name;
            //Age = age;
            //EmployeeID = employeeId;
        }

        public Employee(float salary, string name, int age, int employeeId)
        {
            Console.WriteLine("4 parameter employee ctor is called");
            Salary = salary;
            Name = name;
            Age = age;
            EmployeeID = employeeId;
        }

        private string ssn = "\"you don't know\"";
    }
}
