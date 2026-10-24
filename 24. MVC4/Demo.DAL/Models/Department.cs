using Demo.DAL.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Demo.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Code is required!!!!!!!")]
        public string? Code { get; set; }

        [NoFutureDate(ErrorMessage = "No Future Date")]
        public DateTime DateOfCreation { get; set; }

        [NotMapped] public DateTime? StartDate { get; set; }
        [NotMapped] [AfterDate(nameof(StartDate))] public DateTime? EndDate { get; set; }
    }
}
