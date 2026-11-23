using Microsoft.AspNetCore.Identity;

namespace Demo.Core.Entities;

public class AppUser : IdentityUser
{
    public string? FullName { get; set; }
}