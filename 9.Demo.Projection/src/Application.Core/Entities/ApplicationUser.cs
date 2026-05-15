namespace Application.Core;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}