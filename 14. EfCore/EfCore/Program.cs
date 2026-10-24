using Microsoft.EntityFrameworkCore;

namespace EfCore
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var context = new ApplicationDbContext();




            //List<Employee> employees = new List<Employee>();
            //for (int i = 0; i < 1000; i++)
            //{
            //    var employee = new Employee()
            //    {
            //        Name = $"Name {i}",
            //        Salary = Random.Shared.Next(1000, 5001)
            //    };
            //    employees.Add(employee);
            //}

            //context.Employees.AddRange(employees);
            //context.SaveChanges();

            //var newEmployee = new Employee()
            //{
            //    Name = "Eslam",
            //};

            //context.Employees.Add(newEmployee);


            var employees = context.Employees.AsNoTracking().OrderBy(e => e.Name).ToList();

            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Name + " " + employee.Id);
            }

            //var eslam = context.Employees.Find(2);

            //eslam.Salary = 1333.455m;

            //context.Employees.Update(eslam);

            //context.SaveChanges();

            //foreach(var tracker in context.ChangeTracker.Entries())
            //{
            //    Console.WriteLine(tracker.Entity.ToString()  + " " + tracker.State);
            //}    


        }
    }
}
