namespace Demo.DAL.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName { get; set; }
    }
}
