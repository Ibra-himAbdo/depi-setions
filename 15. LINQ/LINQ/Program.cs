using ConsoleTables;
using SampleData;
using System.Net.WebSockets;
using System.Text.Json;
using System.Xml.Serialization;

namespace LINQ
{
    // Ext methods
    // Generics
    // delegates 
    // Enumarables

    // Linq : https://www.youtube.com/watch?v=gnF7TcvugfA&list=PL62tSREI9C-c8OrLP_atJC_2PXqHKnUme


    public static class EmplyeeExt
    {
        public static List<T> Filter<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            var newLsit = new List<T>();
            foreach (var item in items)
            {
                if (predicate(item))
                    newLsit.Add(item);
            }

            return newLsit;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Employee> emps = Repository.GetEmployees();

            //var result = emps.Where(
            //    employee => employee.Salary >= 8000
            //    && employee.Age > 30
            //    && !employee.IsActive
            //);


            //foreach (var item in result)
            //{
            //    Console.WriteLine("--------");
            //    Console.WriteLine(item);
            //    Console.WriteLine("--------");
            //}

            //emps.Add (new Employee()
            //{
            //    Id = 15556,
            //    Name = "sdads",
            //    Salary = 98000,
            //    Age = 70,
            //    IsActive = false,
            //});

            //Console.WriteLine("---------------888888888999999999----------");

            //foreach (var item in result)
            //{
            //    Console.WriteLine("--------");
            //    Console.WriteLine(item);
            //    Console.WriteLine("--------");
            //}


            //var q14 = emps.Where(e => e.DepartmentId == 30);

            //foreach (var item in q14)
            //{
            //    Console.WriteLine("--------");
            //    Console.WriteLine(item);
            //    Console.WriteLine("--------");
            //}

            //var q15 = emps.Where(employee => employee.Age < 25 && employee.Salary < 10_000);

            //var query26 = emps.Where(x => x.Salary < 10_000)
            //    .Select(e => new
            //{
            //    e.Name,
            //    Age = e.Age.ToString(),
            //    Money = e.Salary
            //});

            //foreach (var item in query26)
            //{
            //    Console.WriteLine("--------");
            //    Console.WriteLine(item);
            //    Console.WriteLine("--------");
            //}

            //var query26 = emps.Where(x => x.Salary < 10_000)
            //    .Select(e => new
            //    {
            //        e.Name,
            //        Age = e.Age.ToString(),
            //        Money = e.Salary,
            //        AnaulSalary = e.Salary * 12m,
            //        SalaryAfterRise = e.Salary * 1.10m,
            //        AgeAfeter5Years = e.Age + 5,
            //    }); 

            //foreach (var item in query26)
            //{
            //    Console.WriteLine("--------");
            //    Console.WriteLine(item);
            //    Console.WriteLine("--------");
            //}

            //IEnumerable<string> query8987 = emps.SelectMany(e => e.Skills).Distinct();

            //IEnumerable<int> query8987 = emps.Select(e => e.Age);

            //foreach (var item in query8987)
            //{
            //    Console.WriteLine(item);
            //    //foreach (var item in items)
            //    //{
            //    //}
            //        //Console.WriteLine(string);
            //}


            //emps.Add(new NewEmp()
            //{
            //    Id = 15556,
            //    Name = "sdads",
            //    Salary = 98000,
            //    Age = 70,
            //    IsActive = false,
            //});

            //var newEmps = emps.OfType<NewEmp>().ToList();

            //foreach (var item in newEmps)
            //{
            //    Console.WriteLine(item);
            //}


            //foreach (var item in emps.Where(e => e.Salary < 10_000 && e.IsActive)
            //    .OrderByDescending(e => e.Age)
            //    .ThenByDescending(e => e.Name)
            //    .Reverse()
            //    .Select(e => new { e.Name, e.Age , e.IsActive }))
            //{
            //    Console.WriteLine(item);
            //}


            //bool isThereAnyNotActiveEmployee = emps.Any(e => !e.IsActive);

            //Console.WriteLine(isThereAnyNotActiveEmployee);

            //bool isThereAllEmployeeIsActive = emps.All(e => e.IsActive);

            //Console.WriteLine(isThereAllEmployeeIsActive);

            //var result = emps.Where(e => e.Salary < 10_000).Count();
            //Console.WriteLine(result);

            //var empto = emps[0];

            //var result = emps.Where(e => e.Age == 28).ToList();
            //Console.WriteLine(result);
            //emps.Clear();

            //Console.WriteLine(emps.OrderBy(e => e.Name).FirstOrDefault(e => e.Age == 28));

            //Console.WriteLine(emps.SingleOrDefault(e => e.Age == 28));

            //Console.WriteLine(emps.Average(e => e.Salary));
            //Console.WriteLine(emps.Max(e => e.Salary));
            //Console.WriteLine(emps.Min(e => e.Salary));
            //Console.WriteLine(emps.Sum(e => e.Salary));


            //foreach (var item in result)
            //{
            //    Console.WriteLine(item);

            //}

            //IEnumerable<Employee[]> empChuncks = emps.Chunk(7);

            //foreach (var item in empChuncks)
            //{
            //    Console.WriteLine(item.Length);

            //}

            //var q11 = emps.GroupBy(e => e.DepartmentId);


            //foreach (var item in q11)
            //{
            //    Console.WriteLine(item.Key);
            //    foreach(var i in item)
            //        Console.WriteLine(i);
            //    Console.WriteLine("------------");
            //}

            var table = new ConsoleTable("Id", "Name", "Age", "Salary", "IsActive", "HireDate", "DepartmentId", "Skills");


            foreach (var employee in emps)
            {
                table.AddRow(employee.Id, employee.Name, employee.Age, employee.Salary,
                    employee.IsActive, employee.HireDate.ToString("yyyy-MMM-dd"), employee.DepartmentId, string.Join(", ", employee.Skills));
            }

            table.Write(Format.MarkDown);

        }



        //static bool IsValidSalary(Employee employee)
        //{
        //    return employee.Salary >= 8000 && employee.Age > 30;
        //}
    }

    class NewEmp : Employee
    {

    }
}
