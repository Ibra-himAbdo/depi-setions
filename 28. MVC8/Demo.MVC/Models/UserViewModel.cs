using System.ComponentModel.DataAnnotations;

namespace Demo.MVC.Models
{
    public class UserViewModel
    {
        [Required]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public bool ChangePassword { get; set; }

        [Required]
        public int Department { get; set; }

        public List<string>? SelectedRoles { get; set; }

        public IFormFile? Image { get; set; }
    }
}
