using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCore
{
    //[Table("Employeeeeeeeeeeeeeeeeees")]
    public class Employee
    {
        //[Key]
        public int Id { get; set; }

        //[Required, MaxLength(30)]
        public string? Name { get; set; }

        //[Column("Salaaaaaaaaaaaaaaaaaary")]
        public decimal Salary { get; set; }

        [NotMapped]
        public DateTime TempDate { get; set; }
        public DateTime CreateAt { get; set; }

        public List<EmployeeCar>? EmployeeCars { get; set; }
    }

    public class Car
    {
        public int Id { get; set; }
        public string? Model { get; set; }

        public List<EmployeeCar>? EmployeeCars { get; set; }
        
    }

    public class EmployeeCar
    {
        public int EmployeeId { get; set; } // fk
        public Employee? Employee { get; set; }

        public int CarId { get; set; } // fk
        public Car? Car { get; set; }
    }
}
