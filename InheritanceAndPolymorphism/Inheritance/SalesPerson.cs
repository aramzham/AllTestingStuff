using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance
{
    class SalesPerson : Employee
    {
        public SalesPerson()
        {
            
        }
        public SalesPerson(string ssn, float salary, string name, int age, int empID, int salesNumber) : base(ssn,salary, name,age,empID)
        {
            SalesNumber = salesNumber;
        }
        //number of sales a salesperson effected
        public int SalesNumber { get; set; }
    }
}
