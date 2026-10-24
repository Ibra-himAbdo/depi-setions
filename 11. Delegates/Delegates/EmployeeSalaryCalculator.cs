namespace Delegates
{
    internal class EmployeeSalaryCalculator
    {
        public event EmployeeSalaryEventHandler? EmployeeSalaryCalculated;

        public delegate void EmployeeSalaryEventHandler(Employee employee, int salary);

        public void CalculateEmployeeSalary(List<Employee> employees, Func<Employee, bool> predicate)
        {
            foreach (Employee employee in employees)
            {
                if (predicate(employee))
                {
                    var salary = employee.BasicSalary - employee.Deductions + employee.Bouns;
                    
                    EmployeeSalaryCalculated?.Invoke(employee, salary);
                }
            }
        }
    }
}
