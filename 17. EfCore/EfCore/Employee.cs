using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EfCore
{
    public class Employee
    {
        //[Key]
        public int Id { get; set; }

        public string? Name { get; set; }
        public int Age { get; set; }

        [NotMapped] public DateTime TempDate { get; set; }
        public DateTime AddOn { get; set; }
        public List<EmployeeCar> EmployeeCars { get; set; } = new List<EmployeeCar>();
    }

    public class Car
    {
        public int Id { get; set; }
        public string? Model { get; set; }

        public List<EmployeeCar> EmployeeCars { get; set; } = new List<EmployeeCar>();
    }

    public class EmployeeCar
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int CarId { get; set; }
        public Car? Car { get; set; }
    }

}