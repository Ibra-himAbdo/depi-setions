using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCore
{
    internal class EmployeeConfigurations : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name)
              .IsRequired()
              .HasMaxLength(25);
            //.HasColumnName("EmployeeName");

            //builder.ToTable("Employeeeeeeeeeees");

            //builder.Ignore(e => e.TempDate);

            //builder.HasKey(e => e.EmployeeKey).HasName("key_asnkdasjkd_kasjdlal");


            builder.Property(e => e.AddOn)
                .HasDefaultValueSql("GETDATE()");

            // 1 to 1
            //builder.HasOne(employee => employee.Car)
            //    .WithOne(car => car.Employee)
            //    .HasForeignKey<Employee>(e => e.CarId);

            // 1 to M
            //builder.HasMany(employee => employee.Cars)
            //    .WithOne(car => car.Employee)
            //    .HasForeignKey(e => e.EmployeeId);

        }
    }
}