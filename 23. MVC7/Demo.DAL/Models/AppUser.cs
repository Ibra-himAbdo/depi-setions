namespace Demo.DAL.Models;

//Employee - Department
public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
}
