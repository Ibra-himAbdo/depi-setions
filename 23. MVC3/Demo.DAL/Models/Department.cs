using System.ComponentModel.DataAnnotations;

namespace Demo.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Code is required!!!!!!!")]
        public string? Code { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
