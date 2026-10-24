using SampleData;
using System.Collections.Immutable;

namespace LINQP2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var emps = Repository.GetEmployees();

            #region DefaredExceution 
            //var filterdEmployees = emps.Where(employee => employee.Age > 38).ToHashSet();

            //foreach (var employee in filterdEmployees)
            //{
            //    Console.WriteLine(employee);
            //}


            //Console.WriteLine();


            //List<Employee> filterdEmployeesFromCustom = new();
            //foreach (var employee in emps)
            //{
            //    if (employee.Age > 38)
            //    {
            //        filterdEmployeesFromCustom.Add(employee);
            //    }

            //}

            //foreach (var employee in filterdEmployeesFromCustom)
            //{
            //    Console.WriteLine(employee);
            //}

            //var newEmployee = new Employee()
            //{
            //    Age = 50,
            //    Name = "Name for Emp with Age 50"
            //};

            //emps.Add(newEmployee);


            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();
            //Console.WriteLine();

            //foreach (var employee in filterdEmployees)
            //{
            //    Console.WriteLine(employee);
            //}

            //Console.WriteLine();
            //foreach (var employee in filterdEmployeesFromCustom)
            //{
            //    Console.WriteLine(employee);
            //} 
            #endregion


            var q119 = emps.GroupBy(employee => employee.DepartmentId);

            //foreach (IGrouping<int, Employee> group in q119)
            //{
            //    Console.WriteLine(group.Key);
            //    Console.WriteLine(group.Count());

            //    foreach (var item in group)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}

            //var q120 = emps.GroupBy(employee => employee.DepartmentId)
            //    .Select(group => new { DepartmentId = group.Key, EmployeeCount = group.Count() });

            //foreach (var item in q120)
            //{
            //    Console.WriteLine(item);
            //}


            //var q122 = emps.GroupBy(employee => employee.DepartmentId)
            //.Select(group => new
            //{
            //    DepartmentId = group.Key,
            //    TotalSalary = group.Sum(employee => employee.Salary),
            //});

            //var q126 = emps.GroupBy(employee => employee.DepartmentId)
            //    .Select(group => new
            //    {
            //        DepartmentId = group.Key,
            //        EmployeeCount = group.Count(e => e.IsActive)
            //    });

            //var q127 = emps.Where(e => e.IsActive)
            //    .GroupBy(employee => employee.DepartmentId)
            //    .Select(group => new
            //    {
            //        DepartmentId = group.Key,
            //        TotalSalary = group.Sum(employee => employee.Salary),
            //    });


            //var q130 = emps.GroupBy(employee => employee.DepartmentId)
            //    .Where(group => group.Count() > 4)
            //    .Select(group => group.Key);

            //var q = emps.GroupBy(employee => employee.Age).OrderBy(g => g.Key);
            //PrintGroup(q);

            //Print(q130);

            // Join

            // select EmpName, DeptNAme form Employee e inner join Depaertment d on e.DpetId = d.Id

            var depts = Repository.GetDepartments();

            var q133 = emps.RightJoin(
                depts,
                emp => emp.DepartmentId,
                dept => dept.Id,
                (employee, department) => new
                    {
                        EmployeeName = employee?.Name,
                        DepartmentName = department?.Name
                    }
                );

            Print(q133);




        }

        static void PrintGroup(IEnumerable<IGrouping<int, Employee>> query)
        {
            foreach (var group in query)
            {
                Console.WriteLine(group.Key);
                foreach (var item in group)
                {
                    Console.WriteLine(item);

                }
                Console.WriteLine();
            }
        }

        static void Print<T>(IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                Console.WriteLine(item);
            }
        }

    }
}
