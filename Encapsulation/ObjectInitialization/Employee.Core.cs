namespace ObjectInitialization
{
    partial class Employee //name the file -name-.-something-.cs to make it partial with -name-.cs file
    {
        public Employee() : this(0,"")
        {
            
        }

        public Employee(int salary, string name)
        {
            Salary = salary;
            Name = name;
        }
        public int Salary { get; set; }
        public string Name { get; set; }
    }
}
