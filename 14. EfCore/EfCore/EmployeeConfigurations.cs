using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCore
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            //builder.ToTable("Employeeeeeeeeeeeeeeeeees");

            //builder.HasKey(e => new { e.Id, e.Name }).HasName("lskadj");

            builder.Property(e => e.Name).IsRequired()
                .HasMaxLength(30);

            builder.Property(e => e.CreateAt).HasDefaultValueSql("getdate()");
            builder.Property(e => e.Salary).HasPrecision(18,4);
            //builder.Property(e => e.Salary).HasColumnType("decimal(18,4)");

            //builder.Property(e => e.Salary).HasColumnName("Salaaaaaaaaaaaaaaaaaary");

            //builder.Ignore(e => e.TempDate);

            // one to one
            //builder.HasOne(e => e.Car)
            //    .WithOne(car => car.Employee)
            //    .HasForeignKey<Employee>(e => e.CarId);

            //// one to many
            //builder.HasMany(e => e.Cars)
            //    .WithOne(car => car.Employee)
            //    .HasForeignKey(car => car.EmployeeId);


        }
    }
}
