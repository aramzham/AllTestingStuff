using System;

namespace Inheritance
{
    class Manager : Employee
    {
        //public Manager(string fullName, int age, int empID, float currentPay, string ssn, int numberOfOptions)
        //{
        //    Console.WriteLine("Manager's full ctor is used");
        //    Name = fullName;
        //    Age = age;
        //    EmployeeID = empID;
        //    Salary = currentPay;
        //    //this.ssn = ssn; // we cannot have access to parent class' private field data
        //    StockOptions = numberOfOptions;
        //}
        public Manager()
        {
            Console.WriteLine("default manager ctor");
        }
        public Manager(string ssn, float currentPay, string fullName, int age, int empID,   int numberOfOptions) : base(ssn,currentPay,fullName,age,empID)
        {
            Console.WriteLine("parametrized manager ctor");
            StockOptions = numberOfOptions;
        }

        //number of stock options a manager has
        public int StockOptions { get; set; }
    }
}
