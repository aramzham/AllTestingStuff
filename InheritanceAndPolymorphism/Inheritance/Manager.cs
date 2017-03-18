using System;

namespace Inheritance
{
    class Manager : Employee
    {
        public Manager(string fullName, int age, int empID, float currentPay, string ssn, int numberOfOptions)
        {
            Console.WriteLine("Manager's full ctor is used");
            Name = fullName;
            Age = age;
            EmployeeID = empID;
            Salary = currentPay;
            //this.ssn = ssn; // we cannot have access to parent class' private field data
            StockOptions = numberOfOptions;
        }
        //number of stock options a manager has
        public int StockOptions { get; set; }
    }
}
