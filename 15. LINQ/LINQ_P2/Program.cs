using SampleData;

// cont. linq
// ADO.NET
// Dapper

namespace LINQ_P2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            List<Employee> emps = Repository.GetEmployees();
            var depts = Repository.GetDepartments();

            // select empName, DeprtName from Employees e inner join Department d on e.dpartID =  d.id
            // inner - left - right

            IEnumerable<Employee> data = emps.Join(
                depts,
                employee => employee.DepartmentId,
                department => department.Id,
                GetFullEmployee);

            foreach (var item in data)
            {
                Console.WriteLine(item);
            }

            //var activeEmps = emps.Where(x => x.IsActive).ToList().ConvertAll(e =>
            //new { e.Name,e.Id });

            //var activeEmps = emps.Select(e =>
            //new { e.Name, e.Id }).ToList();

            //foreach (var item in activeEmps.TakeLast(5))
            //{
            //    Console.WriteLine(item);
            //}

            //IEnumerable<IGrouping<int, Employee>> q119 = emps.GroupBy(e => e.DepartmentId);

            //foreach (IGrouping<int, Employee> group in q119)
            //{
            //    Console.WriteLine(group.Key);
            //    foreach (Employee item in group)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}

            //var q120 = emps.GroupBy(e => e.DepartmentId)
            //    .Select(g => new
            //    {
            //        g.Key,
            //        Count = g.Count()
            //    });

            //foreach (var item in q120)
            //{
            //    Console.WriteLine(item);
            //}

            //var q127 = emps
            //    .GroupBy(e => e.DepartmentId)
            //    .Select(g => new
            //    {
            //        g.Key,
            //        ActiveTotalSalray = g.Count(e => e.IsActive)
            //    });

            //foreach (var item in q127)
            //{
            //    Console.WriteLine(item);
            //}

            //var q132 = emps
            //     .GroupBy(e => e.DepartmentId)
            //     .Where(g => g.Average(e => e.Salary) > 15_000)
            //     .Select(g => new
            //     {
            //         g.Key,
            //         Average = g.Average(e => e.Salary)
            //     });

            //foreach (var item in q132)
            //{
            //    Console.WriteLine(item);
            //}

            //var depts = Repository.GetDepartments();

            //var q133 = emps.Join(depts, employee => employee.DepartmentId, dept => dept.Id,
            //    (emp,department) => new { EmpName = emp.Name, DpetName = department.Name });

            //foreach (var item in q133)
            //{
            //    Console.WriteLine(item);
            //}

            //var number1 = Enumerable.Range(1, 5);
            //var number = Enumerable.Repeat(1, 5);

            //foreach (var item in number)
            //{
            //    Console.WriteLine(item);
            //}
        }

        private static Employee GetFullEmployee(Employee employee, Department department)
        {
            employee.Department = department;

            return employee;
        }
    }
}