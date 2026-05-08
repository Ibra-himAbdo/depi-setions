using Application.Core;
using Application.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application;

public static class DatabaseMigrator
{
    private static List<string> roles = [ApplicationRoles.Admin, ApplicationRoles.User];

    public static async Task MigrateDatabaseAsync(this WebApplication app)
    {
        using IServiceScope scope = app.Services.CreateScope();
        IServiceProvider serviceProvider = scope.ServiceProvider;
        ApplicationDbContext dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        UserManager<ApplicationUser> userManger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager<IdentityRole> roleManger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        await dbContext.Database.MigrateAsync();

        await SeedRoles(roleManger);
        await SeedAdmin(userManger);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        foreach (string role in roles)
        {
            bool roleExists = await roleManager.RoleExistsAsync(role);

            if (!roleExists)
                await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    private static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
    {
        const string email = "hellothere2547@gmail.com";
        const string userName = "Admin";
        const string password = "Password@123";

        ApplicationUser? isAdminCreated = await userManager.FindByEmailAsync(email);

        if (isAdminCreated is not null) return;

        ApplicationUser admin = new()
        {
            FullName = "Admin",
            UserName = userName,
            Email = email,
        };

        var result = await userManager.CreateAsync(admin, password);

        if (result.Succeeded)
        {
            await userManager.AddToRolesAsync(admin, roles);
        }
    }
}