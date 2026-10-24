using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace EfCore
{
    public class ApplicationDbContext : DbContext
    {
        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // old and simple
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SchoolDB2;Integrated Security=True;Connect Timeout=5;Trust Server Certificate=True")
                .LogTo(message =>
                {
                    Console.WriteLine();
                    Console.WriteLine("===========================================");
                    Console.WriteLine(message);
                    Console.WriteLine("===========================================");
                    Console.WriteLine();
                }, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1)
            //modelBuilder.Entity<Employee>()
            //    .Property(e => e.Name)
            //    .IsRequired()
            //    .HasMaxLength(25);

            // 2)
            //new EmployeeConfigurations().Configure(modelBuilder.Entity<Employee>());

            // 3)
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(EmployeeConfigurations).Assembly);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<EmployeeCar>()
                .HasKey(ec => new { ec.EmployeeId, ec.CarId });

            modelBuilder.Entity<EmployeeCar>()
                .HasOne(ec => ec.Employee)
                .WithMany(employee => employee.EmployeeCars)
                .HasForeignKey(ec => ec.EmployeeId);

            modelBuilder.Entity<EmployeeCar>()
                .HasOne(ec => ec.Car)
                .WithMany(car => car.EmployeeCars)
                .HasForeignKey(ec => ec.CarId);
        }


        public DbSet<Employee> Employees { get; set; }
    }
}
