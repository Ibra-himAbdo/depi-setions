using System.ComponentModel.DataAnnotations;

namespace Demo.MVC.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        public string? ReturnUrl { get; set; }
        public bool RemamberMe { get; set; }
    }
}
