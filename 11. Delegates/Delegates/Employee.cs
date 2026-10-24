namespace Delegates
{
    internal class Employee
    {
        public string? Name { get; set; }
        public int BasicSalary { get; set; }
        public int Deductions { get; set; }
        public int Bouns { get; set; }

        public override string ToString()
        {
            return $"Name: {Name} - BasicSalary = {BasicSalary}";
        }
    }
}