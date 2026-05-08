namespace Application.Core;

public class ApplicationUser : IdentityUser , IEntity
{
    public string? FullName { get; set; }
}