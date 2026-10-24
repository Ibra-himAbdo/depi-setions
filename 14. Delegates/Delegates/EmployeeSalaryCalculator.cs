namespace Delegates
{
    internal class EmployeeSalaryCalculator
    {
        public event EmployeeSalaryCalcualtedEventHandler EmployeeSalaryCalcualted;

        public delegate void EmployeeSalaryCalcualtedEventHandler(Employee employee, int salary);


        public void ClculateEmployeeSalary(List<Employee> employees, Predicate<Employee> predicate)
        {
            foreach (Employee employee in employees)
            {
                if (predicate(employee))
                {
                    var salary = employee.BasicSalary + employee.Bouns - employee.Deductions;

                    EmployeeSalaryCalcualted?.Invoke(employee, salary);
                }
            }
        }
    }
}