using Demo.DAL.Data;
using Demo.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Demo.MVC.Extenstions
{
    public static class DatabaseMigrator
    {
        public static async Task MigrateDatabase(this WebApplication application)
        {
            try
            {
                using IServiceScope scope = application.Services.CreateScope();
                AppDbContext context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                UserManager<AppUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                await context.Database.MigrateAsync();

                await SeedRoles(roleManager);
                await SeedAdminUser(userManager);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex);
            }
        }

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            foreach (string role in AppRoles.Roles)
            {
                bool isExists = await roleManager.RoleExistsAsync(role);

                if (!isExists)
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        public static async Task SeedAdminUser(UserManager<AppUser> userManager)
        {
            const string email = "Admin@me";
            const string username = "Admin";
            const string password = "Admin@123";

            AppUser? adminUser = await userManager.FindByEmailAsync(email);

            if (adminUser is not null) return;

            adminUser = new()
            {
                FullName = "Admin",
                Email = email,
                UserName = username,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, password);

            if(result.Succeeded)
            {
                await userManager.AddToRolesAsync(adminUser, AppRoles.Roles);
            }
        }
    }
}