using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EfCore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DbContextOptionsBuilder<ApplicationDbContext> optionsBuilder = new();

            //optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SchoolDB2;Integrated Security=True;Connect Timeout=5;Trust Server Certificate=True")
            //    .LogTo(message =>
            //    {
            //        Console.WriteLine();
            //        Console.WriteLine("===========================================");
            //        Console.WriteLine(message);
            //        Console.WriteLine("===========================================");
            //        Console.WriteLine();
            //    }, LogLevel.Information);

            //var content = new ApplicationDbContext(optionsBuilder.Options);
            var content = new ApplicationDbContext();

            //var employee = new Employee()
            //{
            //    Name = "Test",
            //    Age = 18,
            //};

            //List<Employee> employees = new();
            //for (int i = 0; i < 5; i++)
            //{
            //    var employee2 = new Employee()
            //    {
            //        Name = $"Test {i}",
            //        Age = 18 + i,
            //    };
            //    employees.Add(employee2);
            //}

            //content.Employees.AddRange(employees);


            //var employees = content.Employees.AsNoTracking().ToList();

            ////employee.Name = "DADadADASDASDASDAS88";

            ////Console.WriteLine(employee.Id + " " + employee.Name);

            //var trackers = content.ChangeTracker.Entries();

            //foreach (var tracker in trackers)
            //{
            //    Console.WriteLine(tracker.Entity.ToString() + " " + tracker.State);
            //}


            //var employees = content.Employees.Where(e => e.Id > 5);

            //foreach (var employee in employees)
            //{
            //    Console.WriteLine(employee.Id + " " + employee.Name);

            //}

            var employee = content.Employees.Find(5);

            content.Employees.Remove(employee);


            content.SaveChanges();

        }
    }
}
