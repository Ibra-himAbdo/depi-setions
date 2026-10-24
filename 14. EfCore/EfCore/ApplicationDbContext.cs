using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EfCore
{
    public class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SchoolDB_S1;Integrated Security=True;Connect Timeout=5;Trust Server Certificate=True")
                .LogTo(message =>
                {
                    Console.WriteLine("========================================");
                    Console.WriteLine(message);
                    Console.WriteLine("========================================");
                },
                new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1)
            //modelBuilder.Entity<Employee>()
            //    .Property(e => e.Name)
            //    .IsRequired();

            //2)
            //new EmployeeConfigurations().Configure(modelBuilder.Entity<Employee>());

            //3)
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfigurations).Assembly);

            //4)
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<EmployeeCar> EmployeeCars { get; set; }
    }
}