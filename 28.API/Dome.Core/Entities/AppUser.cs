using Microsoft.AspNetCore.Identity;

namespace Dome.Core.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; } 
}
