using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCore
{
    internal class EmployeeCarConfiguration : IEntityTypeConfiguration<EmployeeCar>
    {
        public void Configure(EntityTypeBuilder<EmployeeCar> builder)
        {
            builder.HasKey(ec => new { ec.EmployeeId, ec.CarId });

            builder.HasOne(ec => ec.Employee)
                .WithMany(employee => employee.EmployeeCars)
                .HasForeignKey(ec => ec.EmployeeId);

            builder.HasOne(ec => ec.Car)
                .WithMany(car => car.EmployeeCars)
                .HasForeignKey(ec => ec.CarId);
        }
    }
}
